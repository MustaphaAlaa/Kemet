import { useEffect } from "react";
import ColorsList from "./ColorsList";
import CreateColor from "./CreateColor";
import Button from "../../Components/ReuseableComponents/Button";
import { MdAddCircle } from "react-icons/md";
import { usePortal } from "../../hooks/usePortal";
import useColorsContext from "../../hooks/useColorsContext";
import { ApiLinks } from "../../APICalls/ApiLinks";


export default function ColorManagement() {
    console.log(`iam inside color management`)
    const { toggle, openPortal, closePortal } = usePortal();

    // const { getColors, colorAdded, colorUpdated, colorDeleted } = useColorsContext();
    // const { getResponseData: getColors, entityAdded: colorAdded, entityUpdated: colorUpdated, entityDeleted: colorDeleted } = useKokoContext<Color>();
    const { getResponseData: getColors, entityAdded: colorAdded, entityUpdated: colorUpdated, entityDeleted: colorDeleted } = useColorsContext();

    useEffect(() => {
        getColors(ApiLinks.getAllColors)

        return () => {

        }

    }, [getColors, colorAdded, colorUpdated, colorDeleted]);



    const OpenPortal = () => openPortal();
    const handleClose = () => closePortal();


    const crateColor = <CreateColor handleClose={handleClose}  ></CreateColor>;

    return <div className="justify-between items-center flex flex-col">
        <Button className="flex flex-row justify-between  gap-3 text-xl" roundedMd success hover onClick={OpenPortal}>إضافة لون <span className="text-white shadow rounded-full border border-2 border-green-200"><MdAddCircle className="text-xl" /> </span></Button>
        {toggle && crateColor}
        <ColorsList  ></ColorsList>
    </div>

}
