import { useState } from "react";
import useColorsContext from "../../hooks/useColorsContext";
import { ColorLabel } from "./ColorLabel";

export default function ColorsList() {

    const { colors } = useColorsContext();

    const [updateModeId, setUpdateModeId] = useState(-1);

    const colorsLabels = colors?.result?.map((item) => <ColorLabel setUpdateModeId={setUpdateModeId} updateModeId={updateModeId} key={item.colorId} color={item}></ColorLabel>);

    return <div className="flex flex-row flex-wrap justify-center">
        {colorsLabels}
    </div>
}








