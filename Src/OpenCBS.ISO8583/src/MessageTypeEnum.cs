using System;

namespace Free.iso8583
{
    public enum MessageVersion : byte
    {
        v1987 = 0,
        v1993 = 1,
        v2003 = 2,
        Reserved3 = 3,
        Reserved4 = 4,
        Reserved5 = 5,
        Reserved6 = 6,
        Reserved7 = 7,
        NationalUse = 8,
        PrivateUse = 9
    }

    public enum MessageClass : byte
    {
        Reserved0 = 0,
        Authorization = 1,
        Financial = 2,
        FileAction = 3,
        Reversal = 4,
        Reconciliation = 5,
        Administrative = 6,
        FeeCollection = 7,
        NetworkManagement = 8,
        Reserved9 = 9
    }

    public enum MessageFunction : byte
    {
        Request = 0,
        RequestResponse = 1,
        Advice = 2,
        AdviceResponse = 3,
        Notification = 4,
        NotificationAcknowledgment = 5,
        Instruction = 6,
        InstructionAcknowledgment = 7,
        ResponseAcknowledgment = 8,
        NegativeAcknowledgment = 9
    }

    public enum MessageOrigin : byte
    {
        Acquirer = 0,
        AcquirerRepeat = 1,
        Issuer = 2,
        IssuerRepeat = 3,
        Other = 4,
        OtherRepeat = 5,
        Reserved6 = 6,
        Reserved7 = 7,
        Reserved8 = 8,
        Reserved9 = 9
    }
}

