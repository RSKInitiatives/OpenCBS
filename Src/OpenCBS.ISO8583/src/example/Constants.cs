/**
 *  Distributed as part of Free.iso8583
 *  
 *  Free.iso8583 is ISO 8583 Message Processor library that makes message parsing/compiling esier.
 *  It will convert ISO 8583 message to a model object and vice versa. So, the other parts of
 *  application will only do the rest effort to make business process done.
 *  
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License or (at your option) any later version. 
 *  See http://gnu.org/licenses/lgpl.html
 *
 *  Developed by AT Mulyana (atmulyana@yahoo.com) 2009-2015
 *  The latest update can be found at sourceforge.net
 **/
using System;

namespace Free.iso8583.example
{
    public static class TransactionTypeCode
    {
        public const String LOGON = "92";
        public const String CASH_WITHDRAWAL = "01";
        public const String BALANCE_INQUIRY = "31";
        public const String TRANSFER_INQUIRY = "39";
        public const String TRANSFER = "40";
        public const String PAYMENT_INQUIRY = "86";
        public const String PAYMENT = "87";
        public const String PIN_CHANGE = "98";
    }

    public enum TransactionTypeCodeBytes : byte
    {
        LOGON = 0x92,
        CASH_WITHDRAWAL = 0x01,
        BALANCE_INQUIRY = 0x31,
        TRANSFER_INQUIRY = 0x39,
        TRANSFER = 0x40,
        PAYMENT_INQUIRY = 0x86,
        PAYMENT = 0x87,
        PIN_CHANGE = 0x98
    }

    public static class AccountCode
    {
        public const String DEFAULT_ACCOUNT = "00";
        public const String SAVING_ACCOUNT = "10";
        public const String CHECKING_ACCOUNT = "20";
        public const String CREDIT_CARD_ACCOUNT = "30";
        public const String CASH = "90";
    }

    public enum AccountCodeBytes : byte
    {
        DEFAULT_ACCOUNT = 0x00,
        SAVINGS_ACCOUNT = 0x10,
        CHECKING_ACCOUNT = 0x20,
        CREDIT_CARD_ACCOUNT = 0x30,
        CASH = 0x90
    }

    public static class PosEntryMediaMode
    {
        public const String UNSPECIFIED = "00";
        public const String MANUAL = "01";
        public const String MAGNETIC_STRIPE = "02";
    }

    public enum PosEntryMediaModeBytes : byte
    {
        UNSPECIFIED = 0x00,
        MANUAL = 0x01,
        MAGNETIC_STRIPE = 0x02
    }

    public static class PosEntryPinMode
    {
        public const String UNSPECIFIED = "0";
        public const String PIN_ENTRY_CAPABILITY = "1";
        public const String NO_PIN_ENTRY_CAPABILITY = "2";
    }

    public enum PosEntryPinModeBytes : byte
    {
        UNSPECIFIED = 0x00,
        PIN_ENTRY_CAPABILITY = 0x01,
        NO_PIN_ENTRY_CAPABILITY = 0x02
    }

    public static class PosConditionCode
    {
        public const String NORMAL_PRESENTMENT = "00";
        public const String CUSTOMER_NOT_PRESENT = "01";
        public const String MAIL_OR_PHONE_ORDER = "08";
        public const String UNATTENDED_TERMINAL = "27";
    }

    public enum PosConditionCodeBytes : byte
    {
        NORMAL_PRESENTMENT = 0x00,
        CUSTOMER_NOT_PRESENT = 0x01,
        MAIL_OR_PHONE_ORDER = 0x08,
        UNATTENDED_TERMINAL = 0x27
    }

    public static class ResponseCode
    {
        public const String COMPLETED_SUCCESSFULLY = "00";
        public const String REFER_TO_CARD_ISSUER = "01";
        public const String INVALID_MERCHANT = "03";
        public const String CAPTURE_CARD = "04";
        public const String UNDEFINED_ERROR = "05";
        public const String INVALID_TRANSACTION = "12";
        public const String INVALID_AMOUNT = "13";
        public const String INVALID_CARD_NUMBER = "14";
        public const String NO_SUCH_ISSUER = "15";
        public const String INVALID_RESPONSE = "20";
        public const String FORMAT_ERROR = "30";
        public const String BANK_NOT_SUPPORTED = "31";
        public const String EXPIRED_CARD = "33";
        public const String RESTRICTED_CARD = "36";
        public const String ALLOWABLE_PIN_TRIES_EXCEEDED = "38";
        public const String NO_CREDIT_ACCOUNT = "39";
        public const String FUNCTION_NOT_SUPPORTED = "40";
        public const String LOST_CARD = "41";
        public const String STOLEN_CARD = "43";
        public const String INSUFFICIENT_FUND = "51";
        public const String NO_CHEQUING_ACCOUNT = "52";
        public const String NO_SAVINGS_ACCOUNT = "53";
        public const String EXPIRED_CARD2 = "54";
        public const String INVALID_PIN = "55";
        public const String CARDHOLDER_NOT_PERMITTED = "57";
        public const String TERMINAL_NOT_PERMITTED = "58";
        public const String EXCEEDS_WITHDRAWAL_AMOUNT_LIMIT = "61";
        public const String RESTRICTED_CARD2 = "62";
        public const String SECURITY_VIOLATION = "63";
        public const String EXCEEDS_WITHDRAWAL_FREQ_LIMIT = "65";
        public const String HARD_CAPTURE = "67";
        public const String RESPONSE_RECEIVED_TOO_LATE = "68";
        public const String TIMEOUT_IN_BILLER = "69";
        public const String OUT_OF_STOCK = "70";
        public const String ALLOWABLE_NUMBER_OF_PIN_TRIES_EXCEEDED = "75";
        public const String INVALID_TO_ACCOUNT = "76";
        public const String INVALID_FROM_ACCOUNT = "77";
        public const String ACCOUNT_IS_CLOSED = "78";
        public const String PHONE_NUMBER_BLOCKED = "79";
        public const String BILL_REFERENCE_NOT_FOUND = "80";
        public const String PHONE_NUMBER_EXPIRED = "81";
        public const String INTERVAL_PAYMENT_TOO_SHORT = "83";
        public const String BILL_ALREADY_PAID = "88";
        public const String LINK_DOWN = "89";
        public const String ISSUER_INOPERATIVE = "91";
        public const String UNABLE_TO_ROUTE_TRANSACTION = "92";
        public const String DUPLICATE_MESSAGE = "94";
        public const String SYSTEM_ERROR = "96";
    }

    public static class ResponseCodeBytes
    {
        public const byte COMPLETED_SUCCESSFULLY = 0x00;
        public const byte REFER_TO_CARD_ISSUER = 0x01;
        public const byte INVALID_MERCHANT = 0x03;
        public const byte CAPTURE_CARD = 0x04;
        public const byte UNDEFINED_ERROR = 0x05;
        public const byte INVALID_TRANSACTION = 0x12;
        public const byte INVALID_AMOUNT = 0x13;
        public const byte INVALID_CARD_NUMBER = 0x14;
        public const byte NO_SUCH_ISSUER = 0x15;
        public const byte INVALID_RESPONSE = 0x20;
        public const byte FORMAT_ERROR = 0x30;
        public const byte BANK_NOT_SUPPORTED = 0x31;
        public const byte EXPIRED_CARD = 0x33;
        public const byte RESTRICTED_CARD = 0x36;
        public const byte ALLOWABLE_PIN_TRIES_EXCEEDED = 0x38;
        public const byte NO_CREDIT_ACCOUNT = 0x39;
        public const byte FUNCTION_NOT_SUPPORTED = 0x40;
        public const byte LOST_CARD = 0x41;
        public const byte STOLEN_CARD = 0x43;
        public const byte INSUFFICIENT_FUND = 0x51;
        public const byte NO_CHEQUING_ACCOUNT = 0x52;
        public const byte NO_SAVINGS_ACCOUNT = 0x53;
        public const byte EXPIRED_CARD2 = 0x54;
        public const byte INVALID_PIN = 0x55;
        public const byte CARDHOLDER_NOT_PERMITTED = 0x57;
        public const byte TERMINAL_NOT_PERMITTED = 0x58;
        public const byte EXCEEDS_WITHDRAWAL_AMOUNT_LIMIT = 0x61;
        public const byte RESTRICTED_CARD2 = 0x62;
        public const byte SECURITY_VIOLATION = 0x63;
        public const byte EXCEEDS_WITHDRAWAL_FREQ_LIMIT = 0x65;
        public const byte HARD_CAPTURE = 0x67;
        public const byte RESPONSE_RECEIVED_TOO_LATE = 0x68;
        public const byte TIMEOUT_IN_BILLER = 0x69;
        public const byte OUT_OF_STOCK = 0x70;
        public const byte ALLOWABLE_NUMBER_OF_PIN_TRIES_EXCEEDED = 0x75;
        public const byte INVALID_TO_ACCOUNT = 0x76;
        public const byte INVALID_FROM_ACCOUNT = 0x77;
        public const byte ACCOUNT_IS_CLOSED = 0x78;
        public const byte PHONE_NUMBER_BLOCKED = 0x79;
        public const byte BILL_REFERENCE_NOT_FOUND = 0x80;
        public const byte PHONE_NUMBER_EXPIRED = 0x81;
        public const byte INTERVAL_PAYMENT_TOO_SHORT =0x83;
        public const byte BILL_ALREADY_PAID = 0x88;
        public const byte LINK_DOWN = 0x89;
        public const byte ISSUER_INOPERATIVE = 0x91;
        public const byte UNABLE_TO_ROUTE_TRANSACTION = 0x92;
        public const byte DUPLICATE_MESSAGE = 0x94;
        public const byte SYSTEM_ERROR = 0x96;
    }

    public static class TlvTag
    {
        public const String DEFAULT_ZERO_LENGTH = "GI";
        public const String TRANSACTION_DATA = "TD";
        public const String INFORMATION_TO_DISPLAY = "ID";
        public const String INFORMATION_TO_PRINT = "IP";
        public const String SOFTWARE_RELEASE = "SR";
    }
}
