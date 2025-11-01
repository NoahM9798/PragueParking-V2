using PragueParking_V2._0;

namespace PragueParkingV2._0_Tests
{
    [TestClass]
    public sealed class ParkingGarage_Tests
    {
        [TestMethod]
        public void FinalPrice_20Minutes_Return30()
        {
            //Arrange
            ParkingGarage garage = new ParkingGarage();
            Data.Config.PricePerHourCar = 30; //Set price per car to 30
            Car car = new Car("TEST123");
            DateTime timeParked = DateTime.Now;
            DateTime timeRetrieved = timeParked.AddMinutes(20);
            //Act
            int finalPrice = garage.FinalPrice(car, timeRetrieved);
            //Assert
            Assert.AreEqual(30, finalPrice);
        }
        [TestMethod]
        public void TryParkVehicle_SpotFull_ReturnFalse()
        {
            //Arrange
            ParkingGarage garage = new ParkingGarage();
            Data.Config.SpotSize = 4; //Set spot size to 4
            garage.AdjustSize(50); //Set garage size to 50 spots
            Car car1 = new Car("CAR001");
            Car car2 = new Car("CAR002");
            //Act
            bool firstParkResult = garage.TryParkVehicle(car1); //This should successfully park at spot 1
            bool secondParkResult = garage.TryParkVehicle(car2, 1); //This should fail as spot 1 will be full
            //Assert
            Assert.IsTrue(firstParkResult);
            Assert.IsFalse(secondParkResult);
        }
    }
}
