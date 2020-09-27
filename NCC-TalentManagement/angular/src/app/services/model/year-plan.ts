const listNam = [];
for (let i = 2014; i <= 2030; i++) {
    listNam.push(
        {
            id: i,
            value: i.toString()
        }
    );
}

const listMonth = [];
for (let i = 1; i <= 12; i++) {
    listMonth.push(
        {
            id: i,
            value: i.toString()
        }
    );
}
const listlevel = [];
for (let i = 0; i <= 10; i++) {
    listlevel.push(
        {
            id: i,
            name: i.toString()
        }
    );
}

const listDegree = [
    {id: 0, value: 'HighSchool'},
    {id: 1, value: 'Bachelor'},
    {id: 2, value: 'Master'},
    {id: 3, value: 'PostDoctor'},
    {id: 4, value: 'Certificate'}
];

const errorLanguage =  {
    vn : [
        {
            value : 406003,
            message : 'xin chao'
        }
    ],
    en : [
        {
            value : 406003,
            message : 'hello'
        }
    ]
}
export class errorMsg {
    static readonly DanhSach = {
        options: errorLanguage
    };
}
export class NamDuan {
    static readonly DanhSach = {
        options: listNam
    };
}
export class Thang {
    static readonly DanhSach = {
        options: listMonth
    };
}
export class Degree {
    static readonly DanhSach = {
        options: listDegree
    };
}
export class Level {
    static readonly DanhSach = {
        options: listlevel
    };
}
export function findErr(errCode : number){
    if(errCode == 4040001){
        return 'child1.errDialog.err4040001'
    }
    if(errCode == 4040002){
        return 'child1.errDialog.err4040002'
    }
    if(errCode == 4040003){
        return 'child1.errDialog.err4040003'
    }
    if(errCode == 4040004){
        return 'child1.errDialog.err4040004'
    }
    if(errCode == 4040005){
        return 'child1.errDialog.err4040005'
    }
    if(errCode == 4030001){
        return 'child1.errDialog.err4030001'
    }
    if(errCode == 4030002){
        return 'child1.errDialog.err4030002'
    }
    if(errCode == 4030003){
        return 'child1.errDialog.err4030003'
    }
    if(errCode == 4040004){
        return 'child1.errDialog.err4040004'
    }
    if(errCode == 4010001){
        return 'child1.errDialog.err4010001'
    }
    if(errCode == 2040001){
        return 'child1.errDialog.err2040001'
    }
    if(errCode == 4060001){
        return 'child1.errDialog.err4060001'
    }
    if(errCode == 406002){
        return 'child1.errDialog.err406002'
    }
    if(errCode == 406003){
        return 'child1.errDialog.err406003'
    }
}
