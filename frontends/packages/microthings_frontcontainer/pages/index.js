import Head from 'next/head'
import Image from 'next/image'
import styles from '../styles/Home.module.css'
import { useState, useEffect } from "react";
import { HubConnectionBuilder, JsonHubProtocol, LogLevel } from '@microsoft/signalr';

import { Home } from '../components/home/home'
import { HomeContent } from '@microthings/home'
import { NotificationsBar } from '@microthings/notifications_bar';

export default function ConnectedHome() {
      NotificationsBar(process.env.NEXT_PUBLIC_GATEWAY_API_ROOT_URL, process.env.NEXT_PUBLIC_NOTIFICATIONS_HUB_URI, "OnLogNotifyPublished");
      useEffect(() => {
          fetch('http://host.docker.internal:5000/weathergate/get')
            .then(response => response.json())

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
