using LINQtoCSV;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.DataMigrator;
using OpenCBS.DataMigrator.FromModel;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.ExceptionsHandler.Exceptions.CustomFieldsExceptions;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Services.Events;
using OpenCBS.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.DataMigrator
{
    class Program
    {
        static User user = null;
        static Branch branch = null;
        static EconomicActivity economicActivity = null;
        static Province province = null;
        static District district = null;
        static City city = null;

        static List<FromModel.MigrationResult> dump = new List<FromModel.MigrationResult>();

        static List<FromModel.Client> migrated = new List<FromModel.Client>();
        static List<FromModel.Client> error = new List<FromModel.Client>();
        static List<FromModel.Client> exist = new List<FromModel.Client>();

        static void Main(string[] args)
        {
            CsvFileDescription fileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            CsvContext cc = new CsvContext();

            var dir = Directory.GetCurrentDirectory();
            
            string sourceFilePath = dir + "/client_table.csv";
            
            Console.WriteLine("***********************************************************");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("****************OpenCBS DATABASE MIGRATOR******************");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("                                                           ");
            Console.WriteLine("**   Ensure that a csv file named \"client_table.csv\"     **");
            Console.WriteLine("**   exists in the bin directory of the migrator before  **");
            Console.WriteLine("**                       proceeding.                     **");
            Console.WriteLine("******            Press 'enter' to continue        ********");
            Console.WriteLine("                                                           ");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("***********************************************************");

            Console.ReadKey();
            try
            {
                ApplicationSettingsServices generalSettingsService = ServicesProvider.GetInstance().GetApplicationSettingsServices();
                generalSettingsService.FillGeneralDatabaseParameter();

                SelectDefaultUser();
                SelectDefaultBranch();
                SelectDefaultEconomicActivity();
                SelectDefaultLocation();

                List<FromModel.Client> clients = cc.Read<FromModel.Client>(sourceFilePath, fileDescription).ToList();
                Console.WriteLine("");
                Console.WriteLine("***********************************************************");
                Console.WriteLine(String.Format("Migrating {0} clients data", clients.Count));
                Console.WriteLine("***********************************************************");
                Console.WriteLine("");

                var _clientService = ServicesProvider.GetInstance().GetClientServices();
                int count = 0;
                MigrationResult result = null;
                foreach (FromModel.Client client in clients)
                {
                    result = new MigrationResult();
                    result.full_name = client.full_name;
                    result.surname = client.surname;
                    result.first_name = client.first_name;

                    try
                    {
                        if (!String.IsNullOrEmpty(client.first_name)
                        ||
                        !String.IsNullOrEmpty(client.surname)
                        ||
                        !String.IsNullOrEmpty(client.full_name))
                        {
                            #region Individual migration
                            if (client.c_type == ClientType.INDIVIDUAL
                                                || client.c_type == ClientType.DIRECTORS
                                                || client.c_type == ClientType.CHILDREN
                                                || client.c_type == ClientType.JOINT
                                                || client.c_type == ClientType.STAFF
                                                || client.c_type == ClientType.CASHIER)
                            {
                                result.type = "INDIVIDUAL";

                                var _query = client.full_name;
                                int onlyActive = 2; //inactive and active
                                int _numbersTotalPage, _numberOfRecords;

                                var results = ServicesProvider.GetInstance().GetClientServices().FindTiers(out _numbersTotalPage, out _numberOfRecords,
                                                _query, onlyActive, 1, 1, 1, 1);

                                if (results.Count > 0)
                                {
                                    exist.Add(client);

                                    result.migrated = "no";
                                    result.failure_reason = "duplicate";
                                    dump.Add(result);
                                    continue;
                                }
                                ++count;
                                Console.WriteLine(count + "). Migrating \"INDIVIDUAL\" " + client.full_name);
                                SavePerson(client);
                                result.migrated = "yes";
                                result.failure_reason = "";
                                dump.Add(result);
                                migrated.Add(client);
                            }
                            #endregion
                            #region Corporate migration
                            else if (client.c_type == ClientType.GOVT
                                                    || client.c_type == ClientType.SOLE_PROPRIETORSHIP
                                                    || client.c_type == ClientType.FINANCIAL_INSTITUTION
                                                    || client.c_type == ClientType.EDUCATIONAL_INSTITUTION
                                                    || client.c_type == ClientType.COMPANY)
                            {
                                result.type = "CORPORATE";

                                var _query = client.full_name;
                                int onlyActive = 2; //inactive and active
                                int _numbersTotalPage, _numberOfRecords;


                                var results = ServicesProvider.GetInstance().GetClientServices().
                                        FindTiersCorporates(onlyActive, 1, out _numbersTotalPage, out _numberOfRecords, _query);

                                if (results.Count > 0)
                                {
                                    exist.Add(client);
                                    result.migrated = "no";
                                    result.failure_reason = "duplicate";
                                    dump.Add(result);
                                    continue;
                                }

                                ++count;
                                Console.WriteLine(count + "). Migrating \"CORPORATE\" " + client.full_name);
                                SaveCorporate(client);
                                result.migrated = "yes";
                                result.failure_reason = "";
                                dump.Add(result);
                                migrated.Add(client);
                            }
                            #endregion
                            #region Group migration
                            else if (client.c_type == ClientType.GROUP
                                || client.c_type == ClientType.CHURCH_MINISTRIES)
                            {
                                result.type = "GROUP";
                                /*var _query = "";
                                int onlyActive = 2; //inactive and active
                                int _numbersTotalPage, _numberOfRecords;

                                var results = ServicesProvider.GetInstance().GetClientServices().FindTiers(out _numbersTotalPage, out _numberOfRecords,
                                                _query, onlyActive, 1, 1, 1, 1);

                                if (results.Count > 0)
                                {
                                    exist.Add(client);
                                    continue;
                                }
                                */
                                //++count;
                                Console.WriteLine(count + "). Migrating \"GROUP\" " + client.full_name);
                                error.Add(client);
                                result.migrated = "no";
                                result.failure_reason = "Requires group members";
                                dump.Add(result);
                                //SaveGroup(client);
                            }
                            #endregion
                        }                        
                    }
                    catch (CustomFieldsAreNotFilledCorrectlyException exc)
                    {
                        Console.WriteLine(CustomExceptionHandler.ShowExceptionText(exc));
                        error.Add(client);
                        result.migrated = "no";
                        result.failure_reason = exc.Message;
                        dump.Add(result);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(CustomExceptionHandler.ShowExceptionText(exc));
                        error.Add(client);
                        result.migrated = "no";
                        result.failure_reason = exc.Message;
                        dump.Add(result);
                    }
                    finally
                    {
                        //dump.Add(result);
                    }
                }

                string resultDir = dir + "/result";
                if (!Directory.Exists(resultDir))
                {
                    Directory.CreateDirectory(resultDir);
                }

                string resultFilePath = resultDir + "/migration.csv";
                string errorFilePath = resultDir + "/error.csv";
                string successfulFilePath = resultDir + "/successful.csv";
                string duplicatesFilePath = resultDir + "/duplicates.csv";

                cc.Write(dump, resultFilePath, fileDescription);
                cc.Write(error, errorFilePath, fileDescription);
                cc.Write(migrated, successfulFilePath, fileDescription);
                cc.Write(exist, duplicatesFilePath, fileDescription);

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("***********************************************************");
                //Console.WriteLine(String.Format("Migrated {0} clients data", migrated.Count));
                Console.WriteLine("Results:");
                Console.WriteLine("Total: " + clients.Count);
                Console.WriteLine("Successful: " + migrated.Count);
                Console.WriteLine("Failed: " + error.Count);
                Console.WriteLine("Duplicates: " + exist.Count);                
                Console.WriteLine("Migration result saved to " + resultDir);
                Console.WriteLine("***********************************************************");


                Console.ReadLine();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception: " + exc);
                Console.ReadLine();
            }
        }

        private static void SelectDefaultUser()
        {
            Console.WriteLine("");
            Console.WriteLine("***********************************************************");

            string userName = "", password = "";
            while (user == null)
            {
                Console.WriteLine("Enter admin user name: ");
                userName = Console.ReadLine();

                Console.WriteLine("Enter admin password: ");
                password = Console.ReadLine();

                user = ServicesProvider.GetInstance().GetUserServices().Find(userName, password);

                if (user == null)
                    Console.WriteLine("Login failed");
                //Console.ReadKey();
                //Process.GetCurrentProcess().Kill();
            }

            User.CurrentUser = user;
            Console.WriteLine("***********************************************************");
            Console.WriteLine("");
        }

        private static void SelectDefaultBranch()
        {
            Console.WriteLine("");
            Console.WriteLine("***********************************************************");
            int branchId = 1;
            var branchService = ServicesProvider.GetInstance().GetBranchService();
            var branches = branchService.FindAllNonDeleted();
            Console.WriteLine("Listing branches found...");
            foreach (var branch in branches)
            {
                Console.WriteLine(branch.ToString());
            }
            while (branch == null)
            {
                Console.Write("Select default branch Id (default = 1?, press enter): ");
                string line = Console.ReadLine();
                if (line != null)
                {
                    if (int.TryParse(line, out branchId))
                    {
                        branch = branchService.FindById(branchId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid branch Id selected");
                    }
                }
                else
                {
                    branch = branchService.FindById(branchId);
                }

                if (branch == null)
                    Console.WriteLine("Selected branch not found");
            }
            Console.WriteLine("***********************************************************");
            Console.WriteLine("");
        }

        private static void SelectDefaultEconomicActivity()
        {
            Console.WriteLine("");
            Console.WriteLine("***********************************************************");

            int economicActivityId = 1;
            var economicActivityService = ServicesProvider.GetInstance().GetEconomicActivityServices();
            var economicActivities = economicActivityService.FindAllEconomicActivities();
            Console.WriteLine("Listing economic activities found...");
            foreach (var economicActivity in economicActivities)
            {
                Console.WriteLine(economicActivity.ToString());
            }
            while (economicActivity == null)
            {
                Console.Write("Select default economic activity Id (default = 1?, press enter): ");
                string line = Console.ReadLine();
                if (line != "")
                {
                    if (int.TryParse(line, out economicActivityId))
                    {
                        economicActivity = economicActivityService.FindEconomicActivity(economicActivityId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid economic activity Id selected");
                    }
                }
                else
                {
                    economicActivity = economicActivityService.FindEconomicActivity(economicActivityId);
                }

                if (branch == null)
                    Console.WriteLine("Selected economic activity not found");
            }

            Console.WriteLine("***********************************************************");
            Console.WriteLine("");
        }

        private static void SelectDefaultLocation()
        {
            Console.WriteLine("");
            Console.WriteLine("***********************************************************");
            var locationService = ServicesProvider.GetInstance().GetLocationServices();
            Console.WriteLine("Listing countries found...");
            var countries = locationService.GetProvinces();
            foreach (var country in countries)
            {
                Console.WriteLine(country.ToString());
            }
            Console.WriteLine("Using default country as \"Nigeria\"");
            province = locationService.FindProvinceByName("Nigeria");
            Console.WriteLine("Using default district as \"ABIA\"");
            district = locationService.FindDistirctByName("ABIA");
            city = locationService.FindCityByDistrictId(district.Id).FirstOrDefault();
            Console.WriteLine("Using default city as \"" + city.Name + "\"");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("");
        }

        private static void SavePerson(FromModel.Client client)
        {
            Person _tempPerson = new Person();
            _tempPerson.FirstName = String.IsNullOrEmpty(client.first_name) ? client.full_name : client.first_name;
            _tempPerson.LastName = String.IsNullOrEmpty(client.surname) ? client.full_name : client.surname;

            _tempPerson.FirstName = String.IsNullOrEmpty(_tempPerson.FirstName) ? " " : _tempPerson.FirstName;
            _tempPerson.LastName = String.IsNullOrEmpty(_tempPerson.LastName) ? " " : _tempPerson.LastName;
            //_tempPerson.FullName = client.full_name;
            _tempPerson.DateOfBirth = DateTime.Now.AddDays(-20);
            _tempPerson.Activity = economicActivity;
            _tempPerson.Sex = client.gender == 1 ? OGender.Male : OGender.Female;
            _tempPerson.RelationshipOfficerId = user.Id;
            _tempPerson.RelationshipOfficer = user;
            _tempPerson.ImagePath = " ";
            _tempPerson.Image2Path = " ";
            _tempPerson.Image3Path = " ";
            _tempPerson.ParticularsIDNumber = client.cid_no.ToString();// "0000000000";
            _tempPerson.ParticularsIssueDate = DateTime.Now;
            _tempPerson.ParticularsExpiryDate = DateTime.Now.AddDays(2);
            _tempPerson.ZipCode = "";
            _tempPerson.HomeType = "";
            _tempPerson.Email = "";
            _tempPerson.District = district;
            _tempPerson.City = city.Name;
            _tempPerson.Address = "";
            _tempPerson.HomePhone = "";
            _tempPerson.PersonalPhone = "";

            _tempPerson.CreationDate = DateTime.Now;

            _tempPerson.SMSDelivery = false;
            _tempPerson.EmailDelivery = true;

            _tempPerson.SecondaryZipCode = "";
            _tempPerson.SecondaryHomeType = "";
            _tempPerson.SecondaryEmail = "";
            _tempPerson.SecondaryDistrict = district;
            _tempPerson.SecondaryCity = city.Name;
            _tempPerson.SecondaryAddress = "";
            _tempPerson.SecondaryHomePhone = "";
            _tempPerson.SecondaryPersonalPhone = "";
            _tempPerson.Branch = branch;
            _tempPerson.CreatedBy = User.CurrentUser;

            bool save = 0 == _tempPerson.Id;
            if (_tempPerson.FirstName != null)
                _tempPerson.FirstName = _tempPerson.FirstName.Trim();
            if (_tempPerson.LastName != null)
                _tempPerson.LastName = _tempPerson.LastName.Trim();
            if (_tempPerson.FatherName != null)
                _tempPerson.FatherName = _tempPerson.FatherName.Trim();
            if (_tempPerson.IdentificationData != null)
                _tempPerson.IdentificationData = _tempPerson.IdentificationData.Trim();
            _tempPerson.Nationality = province.Name;

            _tempPerson.IsNew = false;
            _tempPerson.IsUpdated = true;

            string result = ServicesProvider
                .GetInstance()
                .GetClientServices()
                .SavePerson(ref _tempPerson, (tx, id) =>
                {
                    //foreach (var extension in Extensions) extension.Save(_tempPerson, tx);
                });

            EventProcessorServices es = ServicesProvider.GetInstance().GetEventProcessorServices();
            es.LogClientSaveUpdateEvent(_tempPerson, save);

            if (result != string.Empty)
                Console.WriteLine(result);
        }

        private static void SaveCorporate(FromModel.Client client)
        {
            Corporate _corporate = new Corporate();
            _corporate.CreationDate = TimeProvider.Now;
            _corporate.RegistrationDate = DateTime.Now;
            _corporate.AgrementSolidarity = false;
            _corporate.Activity = economicActivity;

            _corporate.ZipCode = "";
            _corporate.HomeType = "";
            _corporate.Email = "";
            _corporate.District = district;
            _corporate.City = city.Name;
            _corporate.Address = "";
            _corporate.HomePhone = "";
            _corporate.PersonalPhone = "";

            _corporate.Name = client.full_name;
            _corporate.SmallName = client.searchname;

            _corporate.Branch = branch;
            _corporate.CreatedBy = user;
            if (_corporate.Name != null)
                _corporate.Name = _corporate.Name.Trim();
            _corporate.SmallName = _corporate.SmallName.Trim();

            EventProcessorServices es = ServicesProvider.GetInstance().GetEventProcessorServices();
            FundingLine _fundingLine = null;

            _corporate.Id = ServicesProvider
                .GetInstance()
                .GetClientServices()
                .SaveCorporate(_corporate, _fundingLine, tx =>
                {
                    //foreach (var extension in Extensions) extension.Save(_corporate, tx);
                });
            es.LogClientSaveUpdateEvent(_corporate, true);
        }

        private static void SaveGroup(FromModel.Client client)
        {
            Group group = new Group();
            group.Name = client.full_name;
            group.EstablishmentDate = DateTime.Now;
            group.CreationDate = DateTime.Now;
            group.Activity = economicActivity;
            group.District = district;
            group.City = city.Name;
            group.Address = "";
            group.HomePhone = "";
            group.HomeType = "";
            group.PersonalPhone = "";
            group.Email = "";
            group.ZipCode = "";

            group.SecondaryDistrict = district;
            group.SecondaryCity = city.Name;
            group.SecondaryAddress = "";
            group.SecondaryHomePhone = "";
            group.SecondaryHomeType = "";
            group.SecondaryPersonalPhone = "";
            group.SecondaryEmail = "";
            group.SecondaryZipCode = "";
            group.MeetingDay = null;
            group.Branch = branch;
            group.CreatedBy = user;
            bool save = 0 == group.Id;
            if (group.Name != null)
                group.Name = group.Name.Trim();

            string result = ServicesProvider
                .GetInstance()
                .GetClientServices()
                .SaveSolidarityGroup(ref group, (tx, id) =>
                {
                    //foreach (var extension in Extensions) extension.Save(group, tx);
                });

            EventProcessorServices es = ServicesProvider.GetInstance().GetEventProcessorServices();

            es.LogClientSaveUpdateEvent(group, save);
            migrated.Add(client);


        }

        private static void SaveVillage(FromModel.Client client)
        {
            Village _village = new Village();
            _village.Name = client.full_name;
            _village.EstablishmentDate = DateTime.Now;
            _village.CreationDate = DateTime.Now;

            _village.District = district;
            _village.City = city.Name;
            _village.Address = "";
            _village.ZipCode = "";
            _village.LoanOfficer = user;
            _village.MeetingDay = null;
            _village.Branch = branch;
            bool save = 0 == _village.Id;

            _village.Name = _village.Name.Trim();
            _village.Id = ServicesProvider
                .GetInstance()
                .GetClientServices()
                .SaveNonSolidarityGroup(_village, (tx, id) =>
                {
                        //foreach (var extension in Extensions) extension.Save(_village, tx);
                    });

            ServicesProvider.GetInstance().GetClientServices().SetFavouriteLoanOfficerForVillage(_village);
            EventProcessorServices es = ServicesProvider.GetInstance().GetEventProcessorServices();
            es.LogClientSaveUpdateEvent(_village, save);
            foreach (VillageMember member in _village.Members)
                member.IsSaved = true;
        }

    }
}
