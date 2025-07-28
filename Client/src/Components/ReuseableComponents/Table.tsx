import type { JSX } from "react";

export interface TableConfig {
    label: string;
    render: (item: any) => JSX.Element | string | number;

}



export default function Table({ data, config, keyFn }: { data: any[], config: TableConfig[], keyFn: (item: any) => any }) {
    return (
        <table className="table-auto w-full md:table-fixed border-collapse">
            <thead className=" ">
                <tr className="bg-gray-100  ">
                    {config.map((col, index) => (
                        <th className="  " key={index}>{col.label}</th>
                    ))}
                </tr>
            </thead>
            <tbody className=" ">
                {data.map((row) => (
                    <tr className="bg-white   " key={keyFn(row)}>
                        {config.map((col) => (
                            <td className="table-cell border-b border-gray-200 " key={col.label}>{col.render(row)}</td>
                        ))}
                    </tr>
                ))}
            </tbody>
        </table>
    )
}
