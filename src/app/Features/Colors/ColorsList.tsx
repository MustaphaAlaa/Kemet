import type { APIResponse } from "../../Models/APIResponse";
import type { Color } from "../../Models/Color";
import { ColorLabel } from "./ColorLabel";



export default function ColorsList({ colors, notifyColorUpdated, colorUpdated }: { colors: APIResponse<Color[]> }) {
    const colorsLabels = colors?.result?.map((item) => <ColorLabel colorUpdated={colorUpdated} notifyColorUpdated={notifyColorUpdated} key={item.colorId} color={item}></ColorLabel>);

    return <div className="flex flex-row flex-wrap">
        {colorsLabels}
    </div>
}








