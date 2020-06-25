/* using EurocomV2.DataLogic;
using Xunit;

namespace TestLab
{
    public class UnitTest
    {
        [Fact]
        public void GetAgreementStatus_Scenario_Returns_True()
        {
            //Arrange
            DataBaseCommand dataBaseCommand = new DataBaseCommand();

            //Act
            var result = dataBaseCommand.GetAgreementStatus("pTest1");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAgreementStatus_Scenario_Returns_False()
        {
            //Arrange
            DataBaseCommand dataBaseCommand = new DataBaseCommand();

            //Act
            var result = dataBaseCommand.GetAgreementStatus("pTest2");

            //Assert
            Assert.False(result);

        }

        [Fact]
        public void UpdateAgreementStatus_Scenario_UpdatingSuccesfull_Returns_True()
        {
            //Arrange 
            DataBaseCommand dataBaseCommand = new DataBaseCommand();

            //Act 
            dataBaseCommand.UpdateAgreementStatus(true, "dTest1");
            var result = dataBaseCommand.GetAgreementStatus("dTest1");
            dataBaseCommand.UpdateAgreementStatus(false, "dTest1");

            //Assert 
            Assert.True(result);
        }

        [Fact]
        public void UpdateAgreementStatus_Scenario_UpdatingSuccesfull_Returns_False()
        {
            //Arrange 
            DataBaseCommand dataBaseCommand = new DataBaseCommand();

            //Act 
            dataBaseCommand.UpdateAgreementStatus(false, "dTest2");
            var result = dataBaseCommand.GetAgreementStatus("dTest2");
            dataBaseCommand.UpdateAgreementStatus(true, "dTest2");

            //Assert 
            Assert.False(result);
        }
    }
}
            */
