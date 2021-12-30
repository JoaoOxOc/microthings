import React from "react";
import NotificationsHub from "../lib/notificationshub";

export default function NotificationsBar(gatewayApiRootUrl, notificationsHubUri, notificationsHubEvent) {
    NotificationsHub(gatewayApiRootUrl, notificationsHubUri, notificationsHubEvent);
  
    return (
        <div>
        </div>
    )
}