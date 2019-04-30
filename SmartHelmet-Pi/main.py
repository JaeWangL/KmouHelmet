import schedule
import time
from signalrcore.hub_connection_builder import HubConnectionBuilder

hub_connection = HubConnectionBuilder().with_url("ws://localhost:5000/gpshub").build()

def sendData():
    hub_connection.send("SendDataAsync", ["8", "GPRMC,161006.425,A,7855.6020,S,13843.8900,E,154.89,84.62,110715,173.1,W,A*30"])

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

# for test
"""
while message != "exit":
    message = input(">> ")
    if message is not None and message is not "" and message is not "exit":
        sendData()
hub_connection.stop()
"""
