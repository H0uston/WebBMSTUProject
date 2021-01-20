import React from "react";
import styles from "./Header.module.css";
import headerIcon from "../../assets/images/logo.png";
import {NavLink} from "react-router-dom";
import LinkButton from "../common/buttons/LinkButton";
import SearchArea from "./SearchArea/SearchArea";
import AccountLogo from "../../assets/images/accountIcon.svg";

const Header = (props) => {

    return (
        <header className={styles.headerComponent}>
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
                    <NavLink className={styles.toAccountLink} to={"/account"}>
                        <img className={styles.accountLogo} src={AccountLogo} alt={""} />
                    </NavLink>
                </>
                :
                <LinkButton to={"/auth"} text={"Вход/Регистрация"} />
            }
        </header>
    )
};

export default Header;