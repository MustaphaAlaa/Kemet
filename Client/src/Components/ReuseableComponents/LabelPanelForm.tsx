import classNames from "classnames";
import {   useState, type ChangeEvent, type ReactNode } from "react";
import LabelPanel from "./LabelPanel";

export default function LabelPanelForm({
  defaultStyle = true,
  className,
  label,
  styleLabel,
  styleChildren,
  styleForm,
  whenSubmit,
  setUpdateMode,
  children,
  updateMode,
  init,
  numbersOnly = true,
}: {
  styleForm?: string
  numbersOnly?: boolean
  defaultStyle?: boolean;
  className?: string;
  styleLabel?: string;
  styleChildren?: string;
  label: ReactNode;
  children: ReactNode;
  setUpdateMode: React.Dispatch<boolean>;
  updateMode: boolean;
  whenSubmit: (value: any | undefined) => void;
  init: any;
}) {


  const [value, setValue] = useState<any | undefined>(init);

  const handelChange = (event: ChangeEvent<HTMLInputElement>): void => {
    const targetValue = event.target.value;
    if (numbersOnly) {
      const regex = /^\d*$/;
      if (regex.test(targetValue)) {
        setValue(targetValue);
      }
    } else {

      setValue(targetValue);
    }
  };



  const handleSubmit = (event) => {
    event.preventDefault();

    if (value == undefined) console.log(`value undefined`);
    else {
      whenSubmit(value);
      setUpdateMode(false);
    }
  };



  const formStyle = classNames(
    "w-full p-4 rounded-md shadow-md/30 border border-3 font-bold text-center focus:outline focus:ring-2",
    {
      "text-blue-800 text-xl focus:ring-sky-500 border-blue-800 bg-white": defaultStyle &&  styleForm == undefined,
    },
    styleForm
  );

  console.log(formStyle)
  console.log(styleForm)

  return (<>
    <LabelPanel
      className={className}
      label={label}
      styleLabel={styleLabel}
      styleChildren={styleChildren ? styleChildren + '' : 'w-full'}

    >
      {!updateMode ? children :
        <form className="w-full" onSubmit={handleSubmit}>
          <input
            type={numbersOnly ? 'number' : 'text'}
            value={value}
            onChange={handelChange}
            className={formStyle}
          />
        </form>}
    </LabelPanel>

    {/* <div className={`${classes}`}>
      <div className={`${labelStyle}`}>{label}</div>
      <div className={`${childrenStyle}`}>{children}</div>
    </div> */}
  </>

  );
}
