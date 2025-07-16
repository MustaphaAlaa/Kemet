import { MdDeleteForever, MdOutlineCreate } from "react-icons/md"

type cardLabelArguments = { [x: string]: any, handleDelete: () => void, handleUpdateMode: () => void }

export function CardLabel({ children, handleDelete, handleUpdateMode }: cardLabelArguments) {
    return <div className="m-2 flex flex-col  justify-between shadow-md/10  rounded-xl p-3 bg-gray-100 font-bold text-indigo-800">

        {children}
        <div className="m-2 flex flex-row  justify-between text-xl     shadow p-1 rounded-xl bg-white font-bold">
            <MdDeleteForever onClick={handleDelete} className="cursor-pointer bg-red-500  rounded-sm  text-white" />
            <MdOutlineCreate onClick={handleUpdateMode} className="cursor-pointer bg-blue-500  rounded-sm  text-white" />
        </div>
    </div>
}
