const baseApiUrl = 'http://localhost:5021/api';

document.getElementById('updateProjectForm')?.addEventListener('submit', async event => {
    event.preventDefault();
    const authToken = localStorage.getItem('token');
    if (!authToken) {
        alert('Você precisa estar logado para alterar projetos.');
        window.location.href = 'index.html';
        return;
    }

    const projetoModificado = {
        id: parseInt(document.getElementById('updateProjectId').value),
        nome: document.getElementById('updateProjectNome').value.trim(),
        descricao: document.getElementById('updateProjectDescricao').value.trim(),
        dataInicio: new Date(document.getElementById('updateProjectDataInicio').value).toISOString(),
        previsaoTermino: new Date(document.getElementById('updateProjectPrevisaoTermino').value).toISOString(),
        orcamentoTotal: parseFloat(document.getElementById('updateProjectOrcamento').value),
        classificacaoDeRisco: document.getElementById('updateProjectClassificacaoRisco').value,
    };

    try {
        const response = await fetch(`${baseApiUrl}/projeto/alterar`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: 'Bearer ' + authToken,
            },
            body: JSON.stringify({ model: projetoModificado }),
        });

        if (!response.ok) {
            const errMsg = await response.text();
            throw new Error(errMsg || 'Erro ao alterar projeto');
        }

        alert('Projeto alterado com sucesso!');
        document.getElementById('updateProjectForm').reset();
        document.getElementById('searchForm').dispatchEvent(new Event('submit'));
    } catch (error) {
        alert(error.message);
    }
});

document.getElementById('logoutBtn')?.addEventListener('click', () => {
    localStorage.removeItem('token');
    window.location.href = 'index.html';
});

document.getElementById('addProjectForm')?.addEventListener('submit', async event => {
    event.preventDefault();
    const authToken = localStorage.getItem('token');
    if (!authToken) {
        alert('Você precisa estar logado para adicionar projetos.');
        window.location.href = 'index.html';
        return;
    }

    const projetoNovo = {
        nome: document.getElementById('projectNome').value.trim(),
        descricao: document.getElementById('projectDescricao').value.trim(),
        dataInicio: new Date(document.getElementById('projectDataInicio').value).toISOString(),
        previsaoTermino: new Date(document.getElementById('projectPrevisaoTermino').value).toISOString(),
        orcamentoTotal: parseFloat(document.getElementById('projectOrcamento').value),
        classificacaoDeRisco: document.getElementById('projectClassificacaoRisco').value,
    };

    try {
        const response = await fetch(`${baseApiUrl}/projeto/inserir`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: 'Bearer ' + authToken,
            },
            body: JSON.stringify({ model: projetoNovo }),
        });

        if (!response.ok) {
            const errText = await response.text();
            throw new Error(errText || 'Erro ao adicionar projeto');
        }

        alert('Projeto adicionado com sucesso!');
        document.getElementById('addProjectForm').reset();
        document.getElementById('searchForm').dispatchEvent(new Event('submit'));
    } catch (error) {
        alert(error.message);
    }
});

document.getElementById('searchForm')?.addEventListener('submit', async event => {
    event.preventDefault();
    const filtroId = document.getElementById('searchId').value.trim();

    let consultaUrl = `${baseApiUrl}/projeto/buscar/${filtroId}`;

    try {
        const response = await fetch(consultaUrl);
        if (!response.ok) throw new Error(`Erro ao procurar pelo projeto de id ${filtroId}`);
        const projetos = await response.json();
        mostrarProjeto(projetos);
    } catch (error) {
        alert(error.message);
    }
});

document.getElementById('searchFormAll')?.addEventListener('submit', async event => {
    event.preventDefault();

    let consultaUrl = `${baseApiUrl}/projeto/buscar`;

    try {
        const response = await fetch(consultaUrl);
        if (!response.ok) throw new Error('Erro ao procurar por todos os projetos');
        const projetos = await response.json();
        mostrarProjetos(projetos);
    } catch (error) {
        alert(error.message);
    }
});

function mostrarProjeto(projeto) {
    const listaElemento = document.getElementById('projectsList');
    listaElemento.innerHTML = '';

    const item = document.createElement('li');

    const dataInicioFormatada = new Date(projeto.dataInicio).toLocaleString();
    const previsaoTerminoFormatada = new Date(projeto.previsaoTermino).toLocaleString();

    item.textContent = `${projeto.nome} - Status: ${projeto.status} - Início: ${dataInicioFormatada} - Previsão Término: ${previsaoTerminoFormatada}`;

    const botaoExcluir = document.createElement('button');
    botaoExcluir.textContent = 'Excluir';
    botaoExcluir.onclick = async () => {
        const authToken = localStorage.getItem('token');
        if (!authToken) {
            alert('Você precisa estar logado para excluir.');
            return;
        }
        if (!confirm(`Confirma exclusão do projeto ${projeto.nome}?`)) return;

        try {
            const resposta = await fetch(`${baseApiUrl}/projeto/excluir/${projeto.id}`, {
                method: 'DELETE',
                headers: { Authorization: 'Bearer ' + authToken },
            });
            if (!resposta.ok) throw new Error('Erro ao excluir projeto');
            alert('Projeto excluído com sucesso!');
            document.getElementById('searchForm').dispatchEvent(new Event('submit'));
        } catch (error) {
            alert(error.message);
        }
    };

    item.appendChild(botaoExcluir);
    listaElemento.appendChild(item);
}

function mostrarProjetos(listaProjetos) {
    const listaElemento = document.getElementById('projectsList');
    listaElemento.innerHTML = '';

    listaProjetos.forEach(projeto => {
        const item = document.createElement('li');

        const dataInicioFormatada = new Date(projeto.dataInicio).toLocaleString();
        const previsaoTerminoFormatada = new Date(projeto.previsaoTermino).toLocaleString();

        item.textContent = `${projeto.nome} - Status: ${projeto.status} - Início: ${dataInicioFormatada} - Previsão Término: ${previsaoTerminoFormatada}`;

        const botaoExcluir = document.createElement('button');
        botaoExcluir.textContent = 'Excluir';
        botaoExcluir.onclick = async () => {
            const authToken = localStorage.getItem('token');
            if (!authToken) {
                alert('Você precisa estar logado para excluir.');
                return;
            }
            if (!confirm(`Confirma exclusão do projeto ${projeto.nome}?`)) return;

            try {
                const resposta = await fetch(`${baseApiUrl}/projeto/excluir/${projeto.id}`, {
                    method: 'DELETE',
                    headers: { Authorization: 'Bearer ' + authToken },
                });
                if (!resposta.ok) throw new Error('Erro ao excluir projeto');
                alert('Projeto excluído com sucesso!');
                document.getElementById('searchForm').dispatchEvent(new Event('submit'));
            } catch (error) {
                alert(error.message);
            }
        };

        item.appendChild(botaoExcluir);
        listaElemento.appendChild(item);
    });
}


document.getElementById('loginForm')?.addEventListener('submit', async event => {
    event.preventDefault();
    const usuario = event.target.username.value.trim();
    const senha = event.target.password.value.trim();

    try {
        const resposta = await fetch(`${baseApiUrl}/autenticacao/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username: usuario, password: senha }),
        });
        if (!resposta.ok) throw new Error('Usuário ou senha inválidos');

        const dados = await resposta.json();
        localStorage.setItem('token', dados.token);
        alert('Login realizado com sucesso!');
        window.location.href = 'projetos.html';
    } catch (error) {
        alert(error.message);
    }
});
