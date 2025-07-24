import type { Size } from "../../app/Models/Size";



export function SizeCircle({ size }: { size: Size; }) {
    return <div className="p-3 text-xl rounded-full border border-3 border-white text-center bg-sky-100">{size.name}</div>;
    // return <div className="bg-gray-300 shadow-md/50 p-4 rounded-full   border border-3 border-white">{size.name}</div>; 
}
