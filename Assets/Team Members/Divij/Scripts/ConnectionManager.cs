using System;
using System.Collections.Generic;
using Divij;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager Instance;

    public PowerPort initialPort;

    private List<(PowerPort output, PowerPort input)> connections = new();

    private void Awake()
    {
        Instance = this;
    }

    public void SelectPort(PowerPort port)
    {
        if (initialPort == null)
        {
            initialPort = port;
            return;
        }

        TryConnect(initialPort, port);
        initialPort = null;
    }

    public void TryConnect(PowerPort a, PowerPort b)
    {
        if (a.isOutput && !b.isOutput)
        {
            connections.Add((a, b));
            Debug.Log("Connected");
        }
        else if (!a.isOutput && b.isOutput)
        {
            connections.Add((b , a ));
            Debug.Log("Connected");
        }
        else
        {
            Debug.Log("Invalid Connection typ;ew");
        }

        UpdatePower();
    }

    public void UpdatePower()
    {
        foreach (var (output, input) in connections)
        {
           WiredGenerator generator = output.GetComponentInParent<WiredGenerator>();
           
            bool hasPower = generator != null && generator.isOn && output.switchOn;
                          
                       if (input.connectedDevice != null) 
                       {
                           IPowered device = input.connectedDevice as IPowered;

                           if (device != null)
                           {
                               device.SetPowered(hasPower);
                           }
                                                             
                       }               
        }
        
        
    }
}


/* 
    A connection manager is probably the easiest way to manage wire connection in the level as it will manage the power distribution and 
    will do a check to make sure that the correct port types have been linked up 
    
    
    Should save both ports in seperate input and output scripts then compare the indexes so that the correct connection is made and then 
    breaking connections and drawing wires can be done
    -----> New version
    Made a list of the connections, the input and output because you can apparently do that. Now they can be saved as a single 'item' in a list
    so seperating them will be much easier when a disconnecting action is made, Also both transforms can be used in the wire drawing script 

    If else check to make sure that there is a single input and single output port and have to check for both ways incase they go from a device 
    to the generator. If incorrect then it must deselect the first port and not make the connection. Need to update the power status after every 
    connection to match the generators power, should maybe make it copy the switches power status instead.
    
*/