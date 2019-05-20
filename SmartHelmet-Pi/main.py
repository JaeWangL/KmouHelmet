import os
import schedule
import serial
import time
from signalrcore.hub_connection_builder import HubConnectionBuilder

hub_connection = HubConnectionBuilder().with_url("ws://localhost:5000/gpshub").build()

# Now consider only type 'GPRMC'
def getGpsData():
    if os.path.exists('/dev/ttyACM0') == True:
        ser = serial.Serial('/dev/ttyACM0', 4800, timeout = 10)
        gps = ser.readline()
        return gps
    return "Invalid Data"

def sendData():
    data = getGpsData()
    if (data.startswith('$GPRMC')):
        hub_connection.send("SendDataAsync", ["8", data])

def main():
    schedule.every(2).seconds.do(sendData)

    hub_connection.build()
    hub_connection.on("ReceivedData", print)
    hub_connection.start()

    while True:
        schedule.run_pending()
        time.sleep(1)
        
if __name__ == "__main__":
    main()