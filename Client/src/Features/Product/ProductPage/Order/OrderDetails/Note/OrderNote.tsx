import { useEffect, useState } from "react";
import { FaEdit } from "react-icons/fa";
import GetData from "../../../../../../APICalls/GetData";
import ApiLinks from "../../../../../../APICalls/ApiLinks";
import UpdateNote from "./UpdateNote";
import { IoCloseCircleSharp } from "react-icons/io5";

export default function OrderNote({ orderId }: { orderId: number }) {
    // const { response } = GetData<string | null>(`${ApiLinks.orders.getOrderNote(orderId)}`);

    const [updateMode, setUpdateMode] = useState(false);
    const [notes, setNotes] = useState<string | null>(null);
    useEffect(() => {
        const getData = () => {
            fetch(`${ApiLinks.orders.getOrderNote(orderId)}`)
                .then(response => response.json())
                .then(response => setNotes(response.result))
        }

        getData();
    }, [])
    console.log('notessssssssssssssssssssssssss',notes)


    return (
        <div className="bg-white p-3 py-1 text-center flex flex-col space-y-1">
            <h3 className="text-2xl">
                <span className="border-b-1 border-b-gray-300">
                    ملاحظات
                </span>
            </h3>
            <div className=" flex flex-col lg:flex-row  space-x-2 items-center">

                <div
                    onClick={() => setUpdateMode(!updateMode)}
                    className={`self-start bg-gray-200 text-xl  
                    p-1 py-3
                    rounded-lg
                    shadow-md/30       
                    cursor-pointer`}
                >
                    {!updateMode ?

                        <FaEdit className="text-green-500" />
                        : <IoCloseCircleSharp className="text-red-500" />
                    }

                </div>
                {updateMode ? <UpdateNote
                    orderId={orderId}
                    notes={notes}
                    closeUpdateMode={setUpdateMode}
                    setNotes={setNotes}></UpdateNote>
                    :


                    <div className="bg-gray-200 shadow-lg/30 w-full rounded-lg px-3 py-2">
                        {
                            (notes == null || notes == '') ?
                                'NO Notes here'
                                : notes
                        }
                    </div>}
            </div>
        </div>
    )
}
