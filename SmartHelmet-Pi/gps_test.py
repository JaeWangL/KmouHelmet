    import serial
    import os
    from decimal import *
    getcontext().prec = 8
    fix = 0
    gps_con = 0

    while  True:
        # check for gps connected on USB0 or USB1
       if gps_con == 0 and os.path.exists('/dev/ttyUSB0') == True:
          ser = serial.Serial('/dev/ttyUSB0',4800,timeout = 10)
          gps_con = 1
          print "connected on USB0"
       elif gps_con == 0 and os.path.exists('/dev/ttyUSB1') == True:
          ser = serial.Serial('/dev/ttyUSB1',4800,timeout = 10)
          gps_con = 1
          print "connected on USB1"
       if gps_con == 1:
          gps = ser.readline()
          if gps[1 : 6] == "GPGGA":
             gps1 = gps.split(',',14)
          if gps[1:6] == "GPGSA":
             fix = int(gps[9:10])
          if gps[1 : 6] == "GPGGA" and len(gps) > 68 and (gps1[3] == "N" or gps1[3] == "S")and fix > 1:
             lat = int(gps[18:20]) + (Decimal(int(gps[20:22]))/(Decimal(60))) + (Decimal(int(gps[23:27]))/(Decimal(360000)))
             if gps[28:29] == "S":
                lat = 0 - lat
             lon = int(gps[30:33]) + (Decimal(int(gps[33:35]))/(Decimal(60))) + (Decimal(int(gps[36:40]))/(Decimal(360000)))
             if gps[41:42] == "W":
                lon = 0 - lon
             print "LAT:" ,lat
             print "LON:",lon
          if gps[1 : 6] == "GPRMC" and fix > 1:
             gps2 = gps.split(',',14)
             print "SPEED:",gps2[7]
             print "ANGLE:",gps2[8]
             print ""
