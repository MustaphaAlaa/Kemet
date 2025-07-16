
import type { Color } from "../../app/Models/Color";


// i think it would be better if there a version that fetch a specific color based on ColorId 

export function ColorCircle({
  color,
  size = "p-5",
  cursorPointer,
  ...rest
}: {
  size?: string;
  color: Color;
  cursorPointer?: boolean;

  [x: string]: any;
}) {
  const pointer = cursorPointer ? "cursor-pointer" : "";
  return (
    <div
      {...rest}
      style={{ backgroundColor: `${color.hexacode}` }}
      className={`shadow-md/50   rounded-full ${size} ${pointer} pointer    border-3 border-white`}
    ></div>
  );
}
