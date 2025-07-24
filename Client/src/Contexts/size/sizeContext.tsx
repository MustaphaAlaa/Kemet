import type { Size } from "../../app/Models/Size";
import { createCRUDContext } from "../genericContext";

export const SizeContext = createCRUDContext<Size>();
