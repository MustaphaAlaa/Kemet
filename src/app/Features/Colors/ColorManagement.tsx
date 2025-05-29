import { useCallback, useEffect, useState } from "react";
import ColorsList from "./ColorsList";
import CreateColor from "./CreateColor";
import axios from "axios";
import domain from "../../Models/domain";
import type { APIResponse } from "../../Models/APIResponse";
import type { Color } from "../../Models/Color";
import Button from "../../Components/ReuseableComponents/Button";

export default function ColorManagement() {

    const [colorsResponse, setColorsResponse] = useState<APIResponse<Color[]>>();
    const [isColorAdded, setColorIsAdded] = useState<boolean>(false);
    const [isColorUpdated, setColorIsUpdated] = useState<boolean>(false);
    const [AddColor, setAddColor] = useState(false);

    // const getColors = async () => {
    //     const { data } = await axios.get(`${domain}api/color/index`);
    //     setColorsResponse(data)
    // };

    const getColors = useCallback(async () => {
        const { data } = await axios.get(`${domain}api/color/index`);
        setColorsResponse(data)
    }, []);


    // useEffect(() => { getColors() }, [getColors])

    useEffect(() => { getColors() }, [getColors, isColorAdded, isColorUpdated]);

    return <>
        <Button primary hover onClick={() => setAddColor(!AddColor)}>إضافة لون</Button>
        {AddColor &&
            <CreateColor colorAdded={isColorAdded} notifyColorAdded={setColorIsAdded}></CreateColor>

        }
        <ColorsList colorUpdated={isColorUpdated} notifyColorUpdated={setColorIsUpdated} colors={colorsResponse}></ColorsList>
    </>

}
