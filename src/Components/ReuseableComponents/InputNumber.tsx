import classNames from "classnames";
import { useState, type ChangeEvent, type JSX } from "react";

export default function InputNumber({ initValue, defaultStyle = true , direction = "ltr", styles = "", val, placeholder = "", ...rest }: {
    [x: string]: any;
    initValue?:number;
    defaultStyle?: boolean | undefined;
    direction?: string | undefined;
    styles?: string | undefined;
    val: any;
    placeholder?: string | undefined;
}): JSX.Element {

  

    const classes = classNames('  w-2xs   focus:outline-none   p-2 ', {
        'shadow      focus:ring-2 focus:ring-blue-300   ': defaultStyle,
        'bg-white': styles.search('bg') < 0,
        'text': styles.search('text') < 0,
    }, styles);



    const [value, setValue] = useState<number|string>(initValue ?? "");


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

    return <input   {...numberAttr}   {...rest} type="text" className={classes} />
};
