import classNames from "classnames";
import { useState, type ChangeEvent } from "react";

export default function InputNumber({ direction = "ltr", styles = "", val, placeholder = "", ...rest }) {
    const classes = classNames('shadow w-2xs  bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 p-2 text-sky-600  ', styles);



    const [value, setValue] = useState("");


    function handelChange(event: ChangeEvent<HTMLInputElement>): void {

        const targetValue = event.target.value;
        const regex = /^\d*$/;
        if (regex.test(targetValue)) {
            setValue(targetValue)
            val(targetValue); 
        }
    }

    const numberAttr = {
        placeholder: placeholder,
        dir: direction,
        value,
        onChange: handelChange
    }

    return <input    {...numberAttr}   {...rest} type="text" className={classes} />
};
