import Head from 'next/head'
import Image from 'next/image'
import styles from '../styles/Home.module.css'

import { Home } from '../components/home/home'
import { HomeContent } from '@microthings/home'

export default function ConnectedHome() {
      return (
        <>
        <Home>
        </Home>
        <HomeContent></HomeContent>
        </>
      )
}
