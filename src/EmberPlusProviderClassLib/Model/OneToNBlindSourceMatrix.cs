﻿#region copyright
/*
 * This code is from the Lawo/ember-plus GitHub repository and is licensed with
 *
 * Boost Software License - Version 1.0 - August 17th, 2003
 *
 * Permission is hereby granted, free of charge, to any person or organization
 * obtaining a copy of the software and accompanying documentation covered by
 * this license (the "Software") to use, reproduce, display, distribute,
 * execute, and transmit the Software, and to prepare derivative works of the
 * Software, and to permit third-parties to whom the Software is furnished to
 * do so, all subject to the following:
 *
 * The copyright notices in the Software and this entire statement, including
 * the above license grant, this restriction and the following disclaimer,
 * must be included in all copies of the Software, in whole or in part, and
 * all derivative works of the Software, unless such copies or derivative
 * works are solely in the form of machine-executable object code generated by
 * a source language processor.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT
 * SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE
 * FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */
 #endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace EmberPlusProviderClassLib.Model
{
    /// <summary>
    /// The One To N Blind Source Matrix implements a blind source. The blind source is being used as
    /// a default source to be connected to a target. The blind source is connected when any other source
    /// is disconnected from the target. 
    /// </summary>
    public class OneToNBlindSourceMatrix : Matrix
    {
        public OneToNBlindSourceMatrix(int number,
                            Element parent,
                            string identifier,
                            Dispatcher dispatcher,
                            IEnumerable<Signal> targets,
                            IEnumerable<Signal> sources,
                            Signal blindSource,
                            Node labelsNode,
                            bool? isWritable = true,
                            int? targetCount = null,
                            int? sourceCount = null)
        : base(number, parent, identifier, dispatcher, targets, sources, labelsNode, isWritable, targetCount, sourceCount, blindSource)
        {
        }

        protected override bool ConnectOverride(Signal target, IEnumerable<Signal> sources, ConnectOperation operation)
        {
            if (operation == ConnectOperation.Disconnect)
            {
                target.Connect(new Signal[] { BlindSource }, true);
            }
            else if (operation == ConnectOperation.Connect)
            {
                target.Connect(sources.Take(1), true);
            }
            else if (target.HasConnectedSources)
            {
                if (target.ConnectedSources.Contains(sources.FirstOrDefault()))
                {
                    // Trying to connect a connected source
                    target.Connect(sources.Take(1), true);
                }
                else if(sources.Any() == false)
                {
                    // On "right-click disconnect" in ember viewer sources might be empty
                    target.Connect(new Signal[] { BlindSource }, true);
                }
                else
                {
                    target.Connect(sources.Take(1), true);
                }
            }
            else
            {
                target.Connect(sources.Take(1), true);
            }

            return true;
        }

        public override TResult Accept<TState, TResult>(IElementVisitor<TState, TResult> visitor, TState state)
        {
            return visitor.Visit(this, state);
        }
    }
}