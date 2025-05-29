import useColorsContext from "../../../hooks/useColorsContext";
import type { APIResponse } from "../../Models/APIResponse";
import type { Color } from "../../Models/Color";
import { ColorLabel } from "./ColorLabel";



export default function ColorsList() {
    
        const {colors} = useColorsContext();
    
    const colorsLabels = colors?.result?.map((item) => <ColorLabel   key={item.colorId} color={item}></ColorLabel>);

    return <div className="flex flex-row flex-wrap">
        {colorsLabels}
    </div>
}








