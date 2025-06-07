import classNames from 'classnames'
import { useState } from 'react';



export default function InputText({ styles = "", ...rest }) {
    const classes = classNames(
        `w-2xs  
        bg-white focus:outline-none
        focus:ring-2
        focus:ring-blue-300
        p-2 text-sky-600`,
        styles);

    return <input {...rest} type="text" className={classes} />
};


export function InputTextValChange({ setTextValue, styles = "", ...rest }) {

    const [value, setValue] = useState('');

    const handleOnChange = (event) => {
       const valueChanged  = event.target.value;
       
        setValue(valueChanged)
        setTextValue(valueChanged);
    }

    const classes = classNames(
        `w-2xs  
        bg-white focus:outline-none
        focus:ring-2
        focus:ring-blue-300
        p-2 text-sky-600`,
        styles);

    return <input {...rest} type="text" value={value} onChange={handleOnChange} className={classes} />
};


