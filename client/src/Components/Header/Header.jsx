import React from "react";
import styles from "./Header.module.css";
import headerIcon from "../../assets/images/logo.jpg";
import {NavLink} from "react-router-dom";
import LinkButton from "../common/buttons/LinkButton";
import Search from "./Search/Search";
import FunctionalButton from "../common/buttons/FunctionalButton";

const Header = (props) => {
    return (
        <header className={styles.headerComponent}>
            <NavLink to={"/"} className={styles.logoAndTitle}>
                <img className={styles.logo} src={headerIcon} alt={"logo"}/>
            </NavLink>
            <LinkButton text={"Категории"} to={"/category"} />
            <Search />
            <FunctionalButton text={"Найти"} />
            {props.isAuthenticated ?
                <>
                    <LinkButton text={"Корзина"} />
                    <NavLink to={"/account"}>
                        Аккаунт
                    </NavLink>
                </>
                :
                <LinkButton text={"Вход/Регистрация"} to={"/auth"} />
            }
        </header>
    )
};

export default Header;