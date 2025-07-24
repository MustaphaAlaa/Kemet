import classNames from "classnames";
import type { ReactNode } from "react";

export default function LabelPanel({
  defaultStyle = true,
  className,
  label,
  styleLabel,
  styleChildren,
  children,
}: {
  defaultStyle?: boolean;
  className?: string;
  styleLabel?: string;
  styleChildren?: string;
  label: ReactNode;
  children: ReactNode;
}) {
  const classes = classNames(
    "flex flex-col border  border-3  font-bold text-xl items-center rounded-lg ",
    {
      "text-blue-800 bg-gray-300 text-white border-sky-100": defaultStyle,
    },
    className
  );

  const labelStyle = classNames(
    "w-full p-1",
    {
      "text-center text-sky-800 bg-white ": defaultStyle && styleLabel == undefined,
    },
    styleLabel
  );

  const childrenStyle = classNames(
    "p-3",
    {
      "text-blue-800  ": defaultStyle && styleChildren ==  undefined,
    },
    styleChildren
  );

  return (
    <div className={`${classes}`}>
      <div className={`${labelStyle}`}>{label}</div>
      <div className={`${childrenStyle}`}>{children}</div>
    </div>
  );
}
