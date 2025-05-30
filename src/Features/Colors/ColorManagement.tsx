import { useEffect, useState } from "react";
import ColorsList from "./ColorsList";
import CreateColor from "./CreateColor";
import Button from "../../Components/ReuseableComponents/Button";
import useColorsContext from "../../hooks/useColorsContext";


export default function ColorManagement() {

    const [AddColor, setAddColor] = useState(false);

    const { getColors, colorAdded, colorUpdated, colorDeleted } = useColorsContext();

    useEffect(() => {
        getColors()

        return () => {

        }

    }, [getColors, colorAdded, colorUpdated, colorDeleted]);

    return <div className="justify-between items-center flex flex-col">
        <Button className=" " primary hover onClick={() => setAddColor(!AddColor)}>إضافة لون</Button>
        {AddColor &&
            <CreateColor  ></CreateColor>

        }
        <ColorsList  ></ColorsList>
    </div>

}
