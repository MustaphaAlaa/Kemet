import type { ReactNode } from "react";

export default function SelectedCircle({ children, ...rest }: { children: ReactNode, [x: string]: any }) {


    // const style = "text-white  text-3xl rounded-full bg-white cursor-pointer";
    return <span className="text-white  text-3xl rounded-full bg-white cursor-pointer"  {...rest}>{children}</span>

}