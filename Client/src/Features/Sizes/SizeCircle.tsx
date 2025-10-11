import type { Size } from "../../app/Models/Size";



export function SizeCircle({ size }: { size: Size; }) {
    return <div className="p-3 text-xl rounded-full border-3 border-white text-center bg-sky-100">{size.name}</div>;
}
