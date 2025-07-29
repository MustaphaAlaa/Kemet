import type { JSX } from "react";

export interface TableConfig {
    label: string;
    render: (item: any) => JSX.Element | string | number;

}


interface TableProp { dir?: string, data: any[], config: TableConfig[], keyFn: (item: any) => any }


export default function Table({ dir = 'ltr', data, config, keyFn }: TableProp) {
    return (
        <table dir={dir} className="table-auto text-center w-full md:table-fixed border-collapse shadow-xl/50">
            <thead className="">
                <tr className="bg-gray-700 text-white ">
                    {config?.map((col, index) => (
                        <th className="p-[0.8rem] text-[1.3rem]  " key={index}>{col.label}</th>
                    ))}
                </tr>
            </thead>
            <tbody className=" ">
                {data?.map((row) => (
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
