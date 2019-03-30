import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:latlong/latlong.dart';
import 'package:signalr_client/hub_connection.dart';
import 'package:signalr_client/hub_connection_builder.dart';

class HomeScreen extends StatefulWidget {
  @override
  _HomeScreenState createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
   HubConnection _hubConnection;

  _HomeScreenState() {
    openChatConnection();
  }

  Future<void> openChatConnection() async {
    if (_hubConnection == null) {
      _hubConnection = HubConnectionBuilder().withUrl("http://localhost:5001/gpshub").build();
      _hubConnection.on("ReceivedData", _handleIncommingChatMessage);
    }

    if (_hubConnection.state != HubConnectionState.Connected) {
      await _hubConnection.start();
    }
  }

  Future<void> sendChatMessage() async {
    await openChatConnection();
    _hubConnection.invoke("SendData", args: [ "1", "GPRMC,161006.425,A,7855.6020,S,13843.8900,E,154.89,84.62,110715,173.1,W,A*30" ]);
  }

  void _handleIncommingChatMessage(List<Object> args){
    final String deviceId = args[0];
    final double latitude = args[1];
    final double longitude = args[2];
    
    debugPrint('deviceId: $deviceId');
    debugPrint('latitude: $latitude');
    debugPrint('longitude: $longitude');
  }

  List<Marker> _markers = <Marker>[
    new Marker(
      width: 80.0,
      height: 80.0,
      point: new LatLng(35.075642, 129.088511),
      builder: (ctx) => new Container(
        child: Icon(
          Icons.radio_button_checked,
          color: Colors.redAccent,
        ),
      ),
    ),
  ];

  _incrementCounter() {
    _markers.add(
      new Marker(
        width: 80.0,
        height: 80.0,
        point: new LatLng(35.076668, 129.088356),
        builder: (ctx) => new Container(
          child: Icon(
            Icons.radio_button_checked,
            color: Colors.blueAccent,
          ),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: <Widget>[
          FlutterMap(
            options: new MapOptions(
                center: new LatLng(35.076195, 129.089590), zoom: 18.0, maxZoom: 20.0),
            layers: [
              new TileLayerOptions(
                maxZoom: 20.0,
                urlTemplate:
                    "https://api.mapbox.com/styles/v1/mapbox/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}",
                additionalOptions: {
                  'accessToken': 'pk.eyJ1Ijoiam5zbTUwNzIiLCJhIjoiY2p0Y2ZveWxyMHcybTRhczBicWZlaWJxdCJ9.yeRW7DwjR_LsOKQz-WJVBQ',
                  'id': 'light-v9',
                },
              ),
              new MarkerLayerOptions(
                markers: _markers
              ),
            ],
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: sendChatMessage,
        tooltip: 'Increment',
        child: Icon(Icons.add),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }
}
