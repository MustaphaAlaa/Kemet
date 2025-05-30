import { createContext, useCallback, useState, type JSX } from "react"
import type { APIResponse } from "../app/Models/APIResponse";
import axios from "axios";
import domain from "../app/Models/domain";
import type { Color } from "../app/Models/Color";
import EditColor from "../Features/Colors/EditColor";



const colorsContext = createContext({});

export function ColorProvider({ children }) {

    const [colorsResponse, setColorsResponse] = useState<APIResponse<Color[]>>();
    const [colorAdded, setColorIsAdded] = useState<boolean>(false);
    const [colorUpdated, setColorIsUpdated] = useState<boolean>(false);
    const [colorDeleted, setColorIsDeleted] = useState<boolean>(false);


    const getColors = useCallback(async () => {
        const { data } = await axios.get(`${domain}api/color/index`);
        setColorsResponse(data)
    }, []);

    const deleteColor = async (colorId: number) => {
        const { data } = await axios.delete(`${domain}api/a/color`, { data: { ColorId: colorId } })
        setColorIsDeleted(!colorDeleted);
        console.log(data);

    }

    const createColor = async ({ colorName, hexacode }: { colorName: string, hexacode: string }) => {
        const { data }: { data: APIResponse<Color[]> } = await axios.post(`${domain}api/a/Color/add`, { Name: colorName, HexaCode: hexacode })

        console.log(data);

        if (data.statusCode === 201)
            setColorIsAdded(!colorUpdated);

        setColorIsAdded(!colorUpdated);

    }



    const updateColor = async ({ colorId, colorName, hexacode }: { colorId: number, colorName: string, hexacode: string }) => {
        const { data }: { data: APIResponse<Color[]> } = await axios.put(`${domain}api/a/Color/`, { ColorId: colorId, Name: colorName, HexaCode: hexacode })


        if (data.statusCode === 200) setColorIsUpdated(!colorUpdated);
    }

    const vals = {
        colors: colorsResponse,
        getColors,
        createColor,
        colorAdded,
        colorUpdated,
        updateColor,
        deleteColor,
        colorDeleted,
    }


    return <colorsContext.Provider value={vals}>
        {children}
    </colorsContext.Provider>

}
 
export default colorsContext;