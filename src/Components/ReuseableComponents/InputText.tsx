import classNames from 'classnames'



export default function InputText({ styles = "", ...rest }) {
    const classes = classNames(
        `w-2xs  
        bg-white focus:outline-none
        focus:ring-2
        focus:ring-blue-300
        p-2 text-sky-600`,
        styles);

    return <input {...rest}  type="text" className={classes} />
};


