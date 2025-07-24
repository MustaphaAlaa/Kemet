import { useState } from "react";
import { ColorLabel } from "./ColorLabel";
import useColorsContext from "../../hooks/useColorsContext";

export default function ColorsList() {

    const { response: colors } = useColorsContext();

    console.log(`inside color list`);

    const [updateModeId, setUpdateModeId] = useState(-1);

    const colorsLabels = colors?.result?.map((item) => <ColorLabel setUpdateModeId={setUpdateModeId} updateModeId={updateModeId} key={item.colorId} color={item}></ColorLabel>);

    return <div className="flex flex-row flex-wrap justify-center">
        {colorsLabels}
    </div>
}








