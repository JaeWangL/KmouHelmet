import websocket
import json

ws = None

def encode_json(obj):
    # All JSON messages must be terminated by the ASCII character 0x1E (record separator).
    # Reference: https://github.com/aspnet/SignalR/blob/release/2.2/specs/HubProtocol.md#json-encoding
    return json.dumps(obj) + chr(0x1E)

def ws_on_error(ws, error):
    print(error)

def ws_on_close(ws):
    print("### Disconnected from SignalR Server ###")

def ws_on_open(ws):
    print("### Connected to SignalR Server via WebSocket ###")
    
    # Do a handshake request for testing connection
    ws.send(encode_json({
        "protocol": "json",
        "version": 1
    }))

    # Call gpshub's send data method
    ws.send(encode_json({
        "type": 1,
        "target": "SendDataAsync",
        "arguments": ["8", "GPRMC,161006.425,A,7855.6020,S,13843.8900,E,154.89,84.62,110715,173.1,W,A*30"]
    }))

if __name__ == "__main__":
    websocket.enableTrace(True)
    ws = websocket.WebSocketApp("ws://localhost:5000/gpshub",
                              on_error = ws_on_error,
                              on_close = ws_on_close)
    ws.on_open = ws_on_open

# Run with while
ws.run_forever()