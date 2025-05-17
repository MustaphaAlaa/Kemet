
import { AiOutlineDownCircle, AiOutlineLeftCircle } from 'react-icons/ai'

export default function OpenClosedLabel({ valueToExpand, expandedValue, click, children }: { valueToExpand: string, click: MouseEventHandler<HTMLDivElement>, expandedValue: string, children: any }) {
    const iconStyle = " text-2xl  rounded-full";


    return <div className="rounded-sm shadow flex items-center justify-content bg-sky-400 p-3 " onClick={click}>
        {
            expandedValue === valueToExpand ?
                <AiOutlineDownCircle className={`${iconStyle} text-white bg-blue-500`} /> :
                <AiOutlineLeftCircle className={`${iconStyle} bg-white`} /> 
        }
        {children}
    </div>
}
