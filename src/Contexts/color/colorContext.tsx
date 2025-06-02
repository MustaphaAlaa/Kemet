import type { Color } from "../../app/Models/Color";
import { createCRUDContext } from "../genericContext";

const ColorContext = createCRUDContext<Color>();

export { ColorContext }