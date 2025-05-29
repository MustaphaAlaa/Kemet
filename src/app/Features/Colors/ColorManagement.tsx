import { useEffect, useState } from "react";
import ColorsList from "./ColorsList";
import CreateColor from "./CreateColor";
import Button from "../../Components/ReuseableComponents/Button";
import useColorsContext from "../../../hooks/useColorsContext";


export default function ColorManagement() {

    const [AddColor, setAddColor] = useState(false);

    const { getColors, isColorAdded, isColorUpdated } = useColorsContext();

    useEffect(() => {
        getColors()

    }, [getColors, isColorAdded, isColorUpdated]);

    return <div>
        <Button primary hover onClick={() => setAddColor(!AddColor)}>إضافة لون</Button>
        {AddColor &&
            <CreateColor  ></CreateColor>

        }
        <ColorsList  ></ColorsList>
    </div>

}
