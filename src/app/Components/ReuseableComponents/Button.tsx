import classNames from "classnames";
export default function Button
  ({ children, primary, secondary, success, danger, warning, roundedXs, roundedLg, roundedMd, roundedSm, roundedFull, outline, hover, ...rest }: {
    [x: string]: any;
    children: boolean;
    primary: boolean;
    secondary: boolean;
    success: boolean;
    danger: boolean;
    warning: boolean;
    roundedFull: boolean;
    roundedXs: boolean;
    roundedSm: boolean;
    roundedMd: boolean;
    roundedLg: boolean;
    outline: boolean;
    hover: boolean;
  }) {


  const count =
    Number(!!primary) +
    Number(!!secondary) +
    Number(!!success) +
    Number(!!danger) +
    Number(!!warning);

  if (count > 1) return new Error(
    "Only button type of (primary,success, danger,secondary, warning) can be added to the button"
  );




// console.log(rest.className)

  const classes = classNames("text-center hover:bg-gradient-to-r hover:-translate-y-1 ease-in-out duration-300 transition-transform hover:shadow-lg/30 ", rest.className, "cursor-pointer flex items-center m-2 px-3 py-1.5 border", {
    "border-blue-600 border-radius-1 bg-blue-500 ": primary,
    "border-gray-600 border-radius-1 bg-gray-500 text-gray": secondary,
    "border-green-600 border-radius-1 bg-green-500 text-green": success,
    "border-yellow-600 border-radius-1 bg-yellow-100 text-yellow-500": warning,
    "border-red-600 border-radius-1 bg-gradient-to-r from-red-500 via-orange-500 to-rose-400 text-white": danger,
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
    "hover:text-green-900 hover:from-green-100 hover:to-green-300": success && hover,
    "hover:text-gray-900 hover:from-gray-100 hover:to-gray-300": secondary && hover,
  });

  return <button {...rest} className={classes}>{children}</button>;
}

