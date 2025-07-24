import classNames from "classnames";
import type { ReactNode } from "react";

interface ButtonArguments {
  children: ReactNode;
  primary?: boolean;
  secondary?: boolean;
  success?: boolean;
  danger?: boolean;
  warning?: boolean;
  roundedFull?: boolean;
  roundedXs?: boolean;
  roundedSm?: boolean;
  roundedMd?: boolean;
  roundedLg?: boolean;
  outline?: boolean;
  hover?: boolean;
  [x:string]: any
}

// const buttonType: {
//   primary: undefined | boolean,
//   secondary: undefined | boolean,
//   success: undefined | boolean,
//   danger: undefined | boolean,
//   warning: undefined | boolean,
// } = {

//   primary: undefined,
//   secondary: undefined,
//   success: undefined,
//   danger: undefined,
//   warning: undefined,

// }



export default function Button
  ({ children, primary, secondary, success, danger, warning, roundedXs, roundedLg, roundedMd, roundedSm, roundedFull, outline, hover, ...rest }: ButtonArguments) {
 

  // buttonType.primary = primary;
  // buttonType.secondary = secondary;
  // buttonType.success = success;
  // buttonType.danger = danger;
  // buttonType.warning = warning;


  const count =
    Number(!!primary) +
    Number(!!secondary) +
    Number(!!success) +
    Number(!!danger) +
    Number(!!warning);

  if (count > 1) return new Error(
    "Only button type of (primary,success, danger,secondary, warning) can be added to the button"
  );

 
 
  const classes = classNames(" text-center hover:bg-gradient-to-r hover:-translate-y-1 ease-in-out duration-300 transition-transform hover:shadow-lg/30 ", rest.className, "cursor-pointer flex items-center m-2 px-3 py-1.5 border", {
    "border-blue-700   border-2 bg-gradient-to-r from-cyan-500   to-purple-600 ": primary,
    "border-gray-600  bg-gradient-to-l from-gray-400 to-gray-800 border border-2 border-white  text-white": secondary,
    "border-white border-3 shadow-md/30  bg-gradient-to-r from-teal-500  to-teal-600 text-cyan-100": success,
    "border-yellow-600   bg-gradient-to-l from-yellow-400 to-orange-200 text-yellow-800": warning,
    "border-red-600  bg-gradient-to-r from-red-500 via-orange-500 to-rose-400 text-white": danger,
    "rounded-full": roundedFull,
    "rounded-xs": roundedXs,
    "rounded-md": roundedMd,
    "rounded-sm": roundedSm,
    "rounded-lg": roundedLg,
    "bg-gradient-to-r from-white to-white via-white  ": outline,
    "text-blue-500": outline && primary,
    "text-white": !outline && primary,
    "text-green-500": outline && success,
    "text-gray-500": outline && secondary,
    "text-yellow-500": outline && warning,
    "text-red-500": outline && danger,
    "hover:text-blue-900 hover:from-blue-100 hover:to-blue-300": primary && hover,
    "hover:text-white hover:from-red-600 hover:via-red-500 hover:to-red-400 transition-all ": danger && hover,
    "hover:text-yellow-900 hover:from-yellow-100 hover:to-yellow-300": warning && hover,
    "hover:text-teal-900 hover:font-bold hover:from-green-300 hover:via-emerald-200 hover:to-teal-200": success && hover,
    "hover:text-gray-900 hover:from-gray-100 hover:to-gray-300": secondary && hover,
  }, rest.styles);

  return <button {...rest} className={classes}>{children}</button>;
}

 