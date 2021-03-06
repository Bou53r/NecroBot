﻿#region using directives

using System.Threading;
using System.Threading.Tasks;
using PoGo.NecroBot.Logic.Tasks;

#endregion

namespace PoGo.NecroBot.Logic.State
{
    public class FarmState : IState
    {
        public async Task<IState> Execute(ISession session, CancellationToken cancellationToken)
        {
            if (session.LogicSettings.EvolveAllPokemonAboveIv || session.LogicSettings.EvolveAllPokemonWithEnoughCandy)
            {
                await EvolvePokemonTask.Execute(session, cancellationToken);
            }

            if (session.LogicSettings.TransferDuplicatePokemon)
            {
                await TransferDuplicatePokemonTask.Execute(session, cancellationToken);
            }

            if (session.LogicSettings.RenameAboveIv)
            {
                await RenamePokemonTask.Execute(session, cancellationToken);
            }

            await RecycleItemsTask.Execute(session, cancellationToken);

            if (session.LogicSettings.UseEggIncubators)
            {
                await UseIncubatorsTask.Execute(session, cancellationToken);
            }

            if (session.LogicSettings.UseGpxPathing)
            {
                await FarmPokestopsGpxTask.Execute(session, cancellationToken);
            }
            else
            {
                await FarmPokestopsTask.Execute(session, cancellationToken);
            }

            return this;
        }
    }
}