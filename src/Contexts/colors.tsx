import { createContext, useCallback, useState } from "react"
import type { APIResponse } from "../app/Models/APIResponse";
import axios from "axios";
import domain from "../app/Models/domain";
import type { Color } from "../app/Models/Color";



type colorContext = {
    colors: APIResponse<Color[]> | undefined;
    getColors: () => Promise<void>;
    isColorAdded: boolean;
    setColorIsAdded: React.Dispatch<React.SetStateAction<boolean>>;
    isColorUpdated: boolean;
    setColorIsUpdated: React.Dispatch<React.SetStateAction<boolean>>;
}


const colorsContext = createContext( "colorContext");

export function ColorProvider({children}) {

    const [colorsResponse, setColorsResponse] = useState<APIResponse<Color[]>>();
    const [isColorAdded, setColorIsAdded] = useState<boolean>(false);
    const [isColorUpdated, setColorIsUpdated] = useState<boolean>(false);
    // const [AddColor, setAddColor] = useState(false);


    const getColors = useCallback(async () => {
        const { data } = await axios.get(`${domain}api/color/index`);
        setColorsResponse(data)
    }, []);



    // useEffect(() => { getColors() }, [getColors, isColorAdded, isColorUpdated]);

    const vals = {
        colors: colorsResponse,
        getColors,
        isColorAdded,
        setColorIsAdded,
        isColorUpdated,
        setColorIsUpdated,
    }


    return <colorsContext.Provider value={vals}>
        {children}
    </colorsContext.Provider>

}


export default colorsContext;