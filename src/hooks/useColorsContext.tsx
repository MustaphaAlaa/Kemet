import { useContext } from "react"
import colorsContext from "../Contexts/colors";
import type { APIResponse } from "../app/Models/APIResponse";
import type { Color } from "../app/Models/Color";




type colorContextType = {
    colors: APIResponse<Color[]> | undefined;
    getColors: () => Promise<void>;
    isColorAdded: boolean;
    setColorIsAdded: React.Dispatch<React.SetStateAction<boolean>>;
    isColorUpdated: boolean;
    setColorIsUpdated: React.Dispatch<React.SetStateAction<boolean>>;
}  



export default function useColorsContext(): colorContextType {
    return useContext(colorsContext);
}