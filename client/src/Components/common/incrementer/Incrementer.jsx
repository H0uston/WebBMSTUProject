import React, {useState} from "react";
import styles from "./Incrementer.module.css";

const Incrementer = ({count, saveCount}) => {
    let [currentCount, setCurrentCount] = useState(count);

    let increment = async () => {
        setCurrentCount(+currentCount + 1);
        saveCount(+currentCount + 1);
    };

    let decrement = () => {
        if (currentCount > 1) {
            setCurrentCount(+currentCount - 1);
            saveCount(+currentCount - 1);
        }
    };

    let handlerOnChange = (newCurrentCount) => {
        if (isNaN(+newCurrentCount) || +newCurrentCount <= 0) {
        } else {
            setCurrentCount(+newCurrentCount);
        }
    };

    return (
        <div className={styles.CartProductAmountContainer}>
            <div className={styles.title}>Количество</div>
            <div className={styles.incremeneter}>
                <button onClick={() => decrement()}>-</button>
                <input value={currentCount} onChange={(e) => handlerOnChange(e.currentTarget.value)} onBlur={() => saveCount(+currentCount)}/>
                <button onClick={() => increment()}>+</button>
            </div>
        </div>
    );
};

export default Incrementer;