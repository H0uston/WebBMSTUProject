import React, {useState} from "react";
import styles from "./Header.module.css";
import headerIcon from "../../assets/images/logo.png";
import {NavLink} from "react-router-dom";
import LinkButton from "../common/buttons/LinkButton";
import SearchArea from "./SearchArea/SearchArea";
import AccountLogo from "../../assets/images/accountIcon.svg";
import FunctionalButton from "../common/buttons/FunctionalButton";


const AccountMenu = ({status, logout, showMenu }) => {

    return (
        <div className={styles.menuContainer + " " + (status ? styles.moveDown : styles.moveUp)}>
            <LinkButton to={"/account"} text={"Редактировать"} />
            <FunctionalButton text={"Выйти"} onClick={() => { logout(); showMenu() }} />
        </div>
    )
};

const Header = (props) => {
    let [isMenuShown, setIsMenuShown] = useState(false);
    let showMenu = () => {
        setIsMenuShown(!isMenuShown);
    };

    return (
        <header className={styles.headerContent}>
            <div className={styles.headerComponent}>
                <NavLink to={"/"} className={styles.logoAndTitle}>
                    <img className={styles.logo} src={headerIcon} alt={"logo"}/>
                </NavLink>
                <LinkButton text={"Категории"} to={"/categories"} />
                <div className={styles.search}>
                    <SearchArea searchString={props.searchString} updateSearchString={props.updateSearchString} />
                    <div onClick={() => props.setCurrentSearchString(props.searchString)}>
                        <LinkButton text={"Найти"}
                                    to={`/search?productName=${encodeURIComponent(props.searchString)}`}/>
                    </div>
                </div>
                {props.isAuthenticated ?
                    <>
                        <LinkButton to={"/cart"} text={"Корзина"} />
                        <div className={styles.toAccountLink} onClick={() => showMenu()}>
                            <img className={styles.accountLogo} src={AccountLogo} alt={""} />
                        </div>
                    </>
                    :
                    <LinkButton to={"/auth"} text={"Вход/Регистрация"} />
                }
            </div>
            { props.isAuthenticated && <AccountMenu status={isMenuShown} logout={props.logout} showMenu={showMenu}/> }
        </header>
    )
};

export default Header;