import { useState, useEffect } from "react";
import { HubConnectionBuilder, JsonHubProtocol, LogLevel } from '@microsoft/signalr';

function NotificationsHub(gatewayApiRootUrl, notificationsHubUri, notificationsHubEvent) {
    useEffect(() => {
        console.log(gatewayApiRootUrl);
          const connection = new HubConnectionBuilder()
            .withUrl(gatewayApiRootUrl + "/subscribe/" + notificationsHubUri)
            .build();
    
          async function start() {
              try {
                 await connection.start();
              } catch (err) {
                 console.log(err);
                 setTimeout(() => start(), 5000);
              }
          };
    
          connection.on(notificationsHubEvent, data => {
              console.log(data);
          });
           
           connection.onclose(async () => {
              await start();
           });
           
           // Start the connection.
           start()
            .then(() => console.log("connected"))
            .catch(error => console.log("signalR server error: ",error));
    
          console.log(connection);
    
          // return () => {
          //   connection.stop();
          // };
    
          return;
      }, []);
}

export default NotificationsHub