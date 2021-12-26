import Head from 'next/head'
import Image from 'next/image'
import styles from '../styles/Home.module.css'
import { useState, useEffect } from "react";
import { HubConnectionBuilder, JsonHubProtocol, LogLevel } from '@microsoft/signalr';

import { Home } from '../components/home/home'
import { HomeContent } from '@microthings/home'

export default function ConnectedHome() {
  useEffect(() => {
    fetch('http://host.docker.internal:5000/weathergate/get')
        .then(response => response.json())
      const connection = new HubConnectionBuilder()
        .withUrl("http://host.docker.internal:5000/subscribe/log-notifications")
        .build();

      async function start() {
          try {
             await connection.start();
          } catch (err) {
             console.log(err);
             setTimeout(() => start(), 5000);
          }
      };

      connection.on("OnLogNotifyPublished", data => {
          console.log(data);
      });
       
       connection.onclose(async () => {
          await start();
       });
       
       // Start the connection.
       start()
        .then(() => console.log("connected"))
        .catch(error => console.log("signalR server error: ",error));
       
       /* this is here to show an alternative to start, with a then
       connection.start().then(() => console.log("connected"));
       */
       
       /* this is here to show another alternative to start, with a catch
       connection.start().catch(err => console.error(err));
       */

      //connection.start().catch(error => console.log(error));

      console.log(connection);

      // return () => {
      //   connection.stop();
      // };

      return;
  }, []);

      return (
        <>
        <Home>
        </Home>
        <HomeContent></HomeContent>
        </>
      )
}
