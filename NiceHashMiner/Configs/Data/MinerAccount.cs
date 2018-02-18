using System;

namespace NiceHashMiner.Configs.Data
{
    [Serializable]
    public class MinerAccount
    {
        public string UserName = null;
        public string FirstName = null;
        public string LastName = null;
        public string UID = null;

        public MinerAccount(string aUserName, string aFirstName, string aLastName, string aUID)
        {
            UserName = aUserName;
            FirstName = aFirstName;
            LastName = aLastName;
            UID = aUID;
        }

        public string getVisibleName()
        {
            if ((FirstName + LastName) != "")
            {
                if (FirstName != "" && FirstName != null)
                {
                    return FirstName + " " + LastName;
                }
                else
                {
                    return LastName;
                }
            }
            else
            {
                return UserName;
            }
        }
    }
}
