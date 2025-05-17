import React, { useState, type ChangeEvent } from 'react'
import classNames from 'classnames'

export default function InputText({ number = false, styles = "", ...rest }) {
    const classes = classNames('shadow w-2xs  bg-white focus:outline-none focus:ring-2 focus:ring-blue-300 p-2 text-sky-600  ', styles);



    let numberAttr = {};
    const [value, setValue] = useState("");
    if (number) {



        function handelChange(event: ChangeEvent<HTMLInputElement>): void {

            const val = event.target.value;
            const regex = /^\d*$/;
            if (regex.test(val)) {
                setValue(val);
                if (val.length == 11) {
                    console.log(event.target.value)

                }
            }
        }


        numberAttr = {
            placeholder: "01*********",
            dir: "ltr",
            value,
            onChange: handelChange
        }
    }
    return <input {...numberAttr}  {...rest} type="text" className={classes} />
};
