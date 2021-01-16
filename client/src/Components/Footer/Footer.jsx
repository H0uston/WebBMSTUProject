import React from "react";
import styles from "./Footer.module.css";
import telegramLogo from "../../assets/images/telegramLogo.png";
import vkLogo from "../../assets/images/vkLogo.png";
import githubLogo from "../../assets/images/githubLogo.png";

const Footer = (props) => {
    return (
        <footer className={styles.footerComponent}>
            <a href={"https://github.com/H0uston/WebBMSTUProject"} className={styles.logo}><img src={githubLogo} alt={""}/></a>
            <a href={"https://t.me/mp4_thread"} className={styles.logo}><img src={telegramLogo} alt={""}/></a>
            <a href={"https://vk.com/koreshock322"} className={styles.logo}><img src={vkLogo} alt={""}/></a>
        </footer>
    )
};

export default Footer;