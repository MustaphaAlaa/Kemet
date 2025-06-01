import { useEffect } from "react";
import ColorsList from "./ColorsList";
import CreateColor from "./CreateColor";
import Button from "../../Components/ReuseableComponents/Button";
import useColorsContext from "../../hooks/useColorsContext";
import { MdAddCircle } from "react-icons/md";
import { usePortal } from "../../hooks/usePortal";


export default function ColorManagement() {

    const { toggle, openPortal, closePortal } = usePortal();

    const { getColors, colorAdded, colorUpdated, colorDeleted } = useColorsContext();

    useEffect(() => {
        getColors()

        return () => {

        }

    }, [getColors, colorAdded, colorUpdated, colorDeleted]);


    
    const OpenPortal = () => openPortal();
    const handleClose = () => closePortal();


    const crateColor = <CreateColor handleClose={handleClose}  ></CreateColor>;

    return <div className="justify-between items-center flex flex-col">
        <Button className="flex flex-row justify-between w-34" roundedMd success hover onClick={OpenPortal}>إضافة لون <span className="text-white shadow rounded-full border border-2 border-green-200"><MdAddCircle className="text-xl" /> </span></Button>
        {toggle && crateColor}
        <ColorsList  ></ColorsList>
    </div>

}
