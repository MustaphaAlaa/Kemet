import { useState, type FormEvent } from "react"
import Button from "../../../../../../Components/ReuseableComponents/Button";
import { IoSaveSharp } from "react-icons/io5";
import ApiLinks from "../../../../../../APICalls/ApiLinks";

export default function UpdateNote({ orderId, notes, setNotes, closeUpdateMode }:
    {
        orderId: number,
        notes: string | null,
        setNotes: React.Dispatch<React.SetStateAction<string | null>>,
        closeUpdateMode: React.Dispatch<React.SetStateAction<boolean>>
    }) {
    const [val, setVal] = useState<string | null>(notes);
    console.log(val)


    const handleSubmit = async (event: FormEvent) => {
        event.preventDefault();

        if (val != null || val != undefined || val != '') {


            fetch(`${ApiLinks.orders.updateOrderNote(orderId)}`, {
                method: "PUT",
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(val)
            })
                .then(res => {
                    if (!res.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return res.json();
                })
                .then(serverResponse => {
                    console.log('serverResponse')
                    console.log(serverResponse)
                    if (serverResponse.isSuccess) {
                        setNotes(serverResponse.result.note);
                    } else {
                        console.error('Update failed:', serverResponse.errorMessages);
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                })
                .finally(() => {
                    closeUpdateMode(false); // Close regardless of success/failure
                });



        }

    }

    return (
        <form onSubmit={handleSubmit} className="w-full flex flex-col space-y-2">
            <textarea
                className="bg-indigo-50 text-center shadow-md/30"
                name="" id="" placeholder="Write Your Notes Here......" value={val ?? ''} onChange={(event) => setVal(event?.target.value)}>



            </textarea>
            <Button success hover styles="text-center text-3xl self-center"> <IoSaveSharp className="text-center" /></Button>
        </form>
    )
}
