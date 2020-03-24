using System.Data;
using System.Data.SqlClient;

namespace EurocomV2.Models
{
    public class AgreementModel
    {
        private SqlConnection dataBaseConnection;
        private AgreementModel agreementModel;

        public AgreementModel()
        {
            agreementModel = new AgreementModel();
        }

        public bool AgreementAccepted { get; private set; }

        public bool GetAgreementStatus()
        {
            agreementModel.DataBaseCommand("GetAgreement");
            return true;
        }

        public void UpdateAgreementStatus(bool agreed)
        {
            agreementModel.DataBaseCommand("UpdateAgreement");
        }
        
        //Helper Methods 
        private void DataBaseConnection()
        {
            dataBaseConnection.Open();

            dataBaseConnection.Close();
        }

        private void DataBaseCommand(string storedProcedureName)
        {
            SqlCommand cmd = new SqlCommand(storedProcedureName, dataBaseConnection);
            cmd.CommandType = CommandType.StoredProcedure;
        }
    }
}
